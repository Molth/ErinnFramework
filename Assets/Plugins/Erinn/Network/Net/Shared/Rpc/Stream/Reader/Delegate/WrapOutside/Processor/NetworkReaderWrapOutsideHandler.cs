//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Read
    /// </summary>
    public static partial class NetworkReaderHandlers
    {
        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader) => _action.Invoke(target);
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                try
                {
                    param0 = reader.Read<T1>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                T11 param10;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                    param10 = reader.Read<T11>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                T11 param10;
                T12 param11;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                    param10 = reader.Read<T11>();
                    param11 = reader.Read<T12>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                T11 param10;
                T12 param11;
                T13 param12;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                    param10 = reader.Read<T11>();
                    param11 = reader.Read<T12>();
                    param12 = reader.Read<T13>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                T11 param10;
                T12 param11;
                T13 param12;
                T14 param13;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                    param10 = reader.Read<T11>();
                    param11 = reader.Read<T12>();
                    param12 = reader.Read<T13>();
                    param13 = reader.Read<T14>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                T11 param10;
                T12 param11;
                T13 param12;
                T14 param13;
                T15 param14;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                    param10 = reader.Read<T11>();
                    param11 = reader.Read<T12>();
                    param12 = reader.Read<T13>();
                    param13 = reader.Read<T14>();
                    param14 = reader.Read<T15>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14);
            }
        }

        /// <summary>
        ///     Processor
        /// </summary>
        private sealed class NetworkReaderWrapOutsideHandler<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : NetworkReaderWrapOutsideHandlerBase<T0>
        {
            /// <summary>
            ///     Inner action
            /// </summary>
            private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> _action;

            /// <summary>
            ///     Structure
            /// </summary>
            /// <param name="action">Inner action</param>
            public NetworkReaderWrapOutsideHandler(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action) => _action = action;

            /// <summary>
            ///     Invoke
            /// </summary>
            /// <param name="target">Target</param>
            /// <param name="reader">NetworkReader</param>
            public override void Invoke(T0 target, NetworkBuffer reader)
            {
                T1 param0;
                T2 param1;
                T3 param2;
                T4 param3;
                T5 param4;
                T6 param5;
                T7 param6;
                T8 param7;
                T9 param8;
                T10 param9;
                T11 param10;
                T12 param11;
                T13 param12;
                T14 param13;
                T15 param14;
                T16 param15;
                try
                {
                    param0 = reader.Read<T1>();
                    param1 = reader.Read<T2>();
                    param2 = reader.Read<T3>();
                    param3 = reader.Read<T4>();
                    param4 = reader.Read<T5>();
                    param5 = reader.Read<T6>();
                    param6 = reader.Read<T7>();
                    param7 = reader.Read<T8>();
                    param8 = reader.Read<T9>();
                    param9 = reader.Read<T10>();
                    param10 = reader.Read<T11>();
                    param11 = reader.Read<T12>();
                    param12 = reader.Read<T13>();
                    param13 = reader.Read<T14>();
                    param14 = reader.Read<T15>();
                    param15 = reader.Read<T16>();
                }
                catch
                {
                    return;
                }

                _action.Invoke(target, param0, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15);
            }
        }
    }
}