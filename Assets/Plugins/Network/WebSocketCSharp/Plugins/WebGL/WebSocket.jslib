var WebSocketLibrary =
{
    $webGLWebSocketManager:
    {
        instances: {},
        lastId: 0,
        onOpen: null,
        onMessage: null,
        onError: null,
        onClose: null
    },

    WebSocketSetOnOpen: function (callback) {
        webGLWebSocketManager.onOpen = callback;
    },

    WebSocketSetOnMessage: function (callback) {
        webGLWebSocketManager.onMessage = callback;
    },

    WebSocketSetOnError: function (callback) {
        webGLWebSocketManager.onError = callback;
    },

    WebSocketSetOnClose: function (callback) {
        webGLWebSocketManager.onClose = callback;
    },

    WebSocketAllocate: function (urlPtr, binaryTypePtr) {
        var url = UTF8ToString(urlPtr);
        var binaryType = UTF8ToString(binaryTypePtr);
        var id = ++webGLWebSocketManager.lastId;
        webGLWebSocketManager.instances[id] = {
            url: url,
            ws: null,
            binaryType: binaryType
        };
        return id;
    },

    WebSocketAddSubProtocol: function (instanceId, protocolPtr) {
        var instance = webGLWebSocketManager.instances[instanceId];
        if (!instance)
            return -1;
        var protocol = UTF8ToString(protocolPtr);
        if (instance.subProtocols == null)
            instance.subProtocols = [];
        instance.subProtocols.push(protocol);
        return 0;
    },

    WebSocketFree: function (instanceId) {
        var instance = webGLWebSocketManager.instances[instanceId];
        if (!instance)
            return 0;
        if (instance.ws !== null && instance.ws.readyState < 2)
            instance.ws.close();
        delete webGLWebSocketManager.instances[instanceId];
        return 0;
    },

    WebSocketConnect: function (instanceId) {
        var instance = webGLWebSocketManager.instances[instanceId];
        if (!instance)
            return -1;
        if (instance.ws !== null)
            return -2;
        if (instance.subProtocols != null)
            instance.ws = new WebSocket(instance.url, instance.subProtocols);
        else
            instance.ws = new WebSocket(instance.url);
        instance.ws.binaryType = instance.binaryType;
        instance.ws.onopen = function () {
            Module.dynCall_vi(webGLWebSocketManager.onOpen, instanceId);
        };

        instance.ws.onmessage = function (ev) {
            if (ev.data instanceof ArrayBuffer) {
                var array = new Uint8Array(ev.data);
                var buffer = _malloc(array.length);
                writeArrayToMemory(array, buffer);
                try {
                    Module.dynCall_viii(webGLWebSocketManager.onMessage, instanceId, buffer, array.length);
                }
                finally {
                    _free(buffer);
                }
            }
            else if (ev.data instanceof Blob) {
                var reader = new FileReader();
                reader.onload = function () {
                    var array = new Uint8Array(reader.result);
                    var buffer = _malloc(array.length);
                    writeArrayToMemory(array, buffer);
                    try {
                        Module.dynCall_viii(webGLWebSocketManager.onMessage, instanceId, buffer, array.length);
                    }
                    finally {
                        reader = null;
                        _free(buffer);
                    }
                };
                reader.readAsArrayBuffer(ev.data);
            }
            else {
                console.log("[JS LIB WebSocket] not support message type: ", (typeof ev.data));
            }
        };

        instance.ws.onerror = function (ev) {
            var msg = "WebSocket error.";
            var length = lengthBytesUTF8(msg) + 1;
            var buffer = _malloc(length);
            stringToUTF8(msg, buffer, length);
            try {
                Module.dynCall_vii(webGLWebSocketManager.onError, instanceId, buffer);
            }
            finally {
                _free(buffer);
            }
        };

        instance.ws.onclose = function (ev) {
            var msg = ev.reason;
            var length = lengthBytesUTF8(msg) + 1;
            var buffer = _malloc(length);
            stringToUTF8(msg, buffer, length);
            try {
                Module.dynCall_viii(webGLWebSocketManager.onClose, instanceId, ev.code, buffer);
            }
            finally {
                _free(buffer);
            }
            instance.ws = null;
        };
        return 0;
    },

    WebSocketClose: function (instanceId, code, reasonPtr) {
        var instance = webGLWebSocketManager.instances[instanceId];
        if (!instance)
            return -1;
        if (instance.ws === null)
            return -3;
        if (instance.ws.readyState === 2)
            return -4;
        if (instance.ws.readyState === 3)
            return -5;
        var reason = (reasonPtr ? UTF8ToString(reasonPtr) : undefined);
        try {
            instance.ws.close(code, reason);
        }
        catch (err) {
            return -7;
        }
        return 0;
    },

    WebSocketSend: function (instanceId, bufferPtr, length) {
        var instance = webGLWebSocketManager.instances[instanceId];
        if (!instance)
            return -1;
        if (instance.ws === null)
            return -3;
        if (instance.ws.readyState !== 1)
            return -6;
        instance.ws.send(buffer.slice(bufferPtr, bufferPtr + length));
        return 0;
    },

    WebSocketGetState: function (instanceId) {
        var instance = webGLWebSocketManager.instances[instanceId];
        if (!instance)
            return -1;
        if (instance.ws === null)
            return 3;
        return instance.ws.readyState;
    }
};

autoAddDeps(WebSocketLibrary, '$webGLWebSocketManager');
mergeInto(LibraryManager.library, WebSocketLibrary);