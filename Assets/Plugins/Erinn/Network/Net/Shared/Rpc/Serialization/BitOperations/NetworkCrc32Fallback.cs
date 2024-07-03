//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Runtime.CompilerServices;

// ReSharper disable RedundantExplicitArraySize
// ReSharper disable UseCollectionExpression
// ReSharper disable GrammarMistakeInComment

namespace Erinn
{
    /// <summary>
    ///     CRC-32 fallback
    /// </summary>
    internal static class NetworkCrc32Fallback
    {
        // CRC-32 transition table.
        // While this implementation is based on the Castagnoli CRC-32 polynomial (CRC-32C),
        // x32 + x28 + x27 + x26 + x25 + x23 + x22 + x20 + x19 + x18 + x14 + x13 + x11 + x10 + x9 + x8 + x6 + x0,
        // this version uses reflected bit ordering, so 0x1EDC6F41 becomes 0x82F63B78u.
        // This is computed lazily so as to avoid increasing the assembly size for data that's
        // only needed on a fallback path.
        private static readonly uint[] Sse42CrcTable = new uint[256]
        {
            0U, 4067132163U, 3778769143U, 324072436U, 3348797215U, 904991772U, 648144872U, 3570033899U, 2329499855U, 2024987596U, 1809983544U, 2575936315U, 1296289744U, 3207089363U, 2893594407U, 1578318884U, 274646895U, 3795141740U, 4049975192U, 51262619U, 3619967088U, 632279923U, 922689671U, 3298075524U, 2592579488U, 1760304291U, 2075979607U, 2312596564U, 1562183871U, 2943781820U, 3156637768U, 1313733451U, 549293790U, 3537243613U, 3246849577U, 871202090U, 3878099393U, 357341890U, 102525238U, 4101499445U, 2858735121U, 1477399826U, 1264559846U, 3107202533U, 1845379342U, 2677391885U, 2361733625U, 2125378298U, 820201905U, 3263744690U, 3520608582U, 598981189U, 4151959214U, 85089709U, 373468761U, 3827903834U, 3124367742U, 1213305469U, 1526817161U, 2842354314U, 2107672161U, 2412447074U, 2627466902U, 1861252501U, 1098587580U, 3004210879U, 2688576843U, 1378610760U, 2262928035U, 1955203488U, 1742404180U, 2511436119U, 3416409459U, 969524848U, 714683780U, 3639785095U, 205050476U, 4266873199U, 3976438427U, 526918040U, 1361435347U, 2739821008U, 2954799652U, 1114974503U, 2529119692U, 1691668175U, 2005155131U, 2247081528U, 3690758684U, 697762079U, 986182379U, 3366744552U, 476452099U, 3993867776U, 4250756596U, 255256311U, 1640403810U, 2477592673U, 2164122517U, 1922457750U, 2791048317U, 1412925310U, 1197962378U, 3037525897U, 3944729517U, 427051182U, 170179418U, 4165941337U, 746937522U, 3740196785U, 3451792453U, 1070968646U, 1905808397U, 2213795598U, 2426610938U, 1657317369U, 3053634322U, 1147748369U, 1463399397U, 2773627110U, 4215344322U, 153784257U, 444234805U, 3893493558U, 1021025245U, 3467647198U, 3722505002U, 797665321U, 2197175160U, 1889384571U, 1674398607U, 2443626636U, 1164749927U, 3070701412U, 2757221520U, 1446797203U, 137323447U, 4198817972U, 3910406976U, 461344835U, 3484808360U, 1037989803U, 781091935U, 3705997148U, 2460548119U, 1623424788U, 1939049696U, 2180517859U, 1429367560U, 2807687179U, 3020495871U, 1180866812U, 410100952U, 3927582683U, 4182430767U, 186734380U, 3756733383U, 763408580U, 1053836080U, 3434856499U, 2722870694U, 1344288421U, 1131464017U, 2971354706U, 1708204729U, 2545590714U, 2229949006U, 1988219213U, 680717673U, 3673779818U, 3383336350U, 1002577565U, 4010310262U, 493091189U, 238226049U, 4233660802U, 2987750089U, 1082061258U, 1395524158U, 2705686845U, 1972364758U, 2279892693U, 2494862625U, 1725896226U, 952904198U, 3399985413U, 3656866545U, 731699698U, 4283874585U, 222117402U, 510512622U, 3959836397U, 3280807620U, 837199303U, 582374963U, 3504198960U, 68661723U, 4135334616U, 3844915500U, 390545967U, 1230274059U, 3141532936U, 2825850620U, 1510247935U, 2395924756U, 2091215383U, 1878366691U, 2644384480U, 3553878443U, 565732008U, 854102364U, 3229815391U, 340358836U, 3861050807U, 4117890627U, 119113024U, 1493875044U, 2875275879U, 3090270611U, 1247431312U, 2660249211U, 1828433272U, 2141937292U, 2378227087U, 3811616794U, 291187481U, 34330861U, 4032846830U, 615137029U, 3603020806U, 3314634738U, 939183345U, 1776939221U, 2609017814U, 2295496738U, 2058945313U, 2926798794U, 1545135305U, 1330124605U, 3173225534U, 4084100981U, 17165430U, 307568514U, 3762199681U, 888469610U, 3332340585U, 3587147933U, 665062302U, 2042050490U, 2346497209U, 2559330125U, 1793573966U, 3190661285U, 1279665062U, 1595330642U, 2910671697U
        };

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, byte data)
        {
            crc = Unsafe.Add(ref Sse42CrcTable[0], (IntPtr)(byte)(crc ^ data)) ^ (crc >> 8);
            return crc;
        }

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, ushort data)
        {
            ref var local = ref Sse42CrcTable[0];
            crc = Unsafe.Add(ref local, (IntPtr)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref local, (IntPtr)(byte)(crc ^ data)) ^ (crc >> 8);
            return crc;
        }

        /// <summary>
        ///     Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32C(uint crc, uint data) => Crc32CCore(crc, data);

        /// <summary>
        ///     Core method for computing the CRC32C checksum using a lookup table.
        /// </summary>
        /// <param name="crc">The current CRC value.</param>
        /// <param name="data">The data to process.</param>
        /// <returns>The updated CRC32C checksum.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Crc32CCore(uint crc, uint data)
        {
            ref var lookupTable = ref Sse42CrcTable[0];
            crc = Unsafe.Add(ref lookupTable, (IntPtr)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (IntPtr)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (IntPtr)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (IntPtr)(byte)(crc ^ data)) ^ (crc >> 8);
            return crc;
        }
    }
}
#endif