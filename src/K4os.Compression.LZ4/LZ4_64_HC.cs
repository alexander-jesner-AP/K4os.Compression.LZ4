﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uint8 = System.Byte;
using uint32 = System.UInt32;
using uint16 = System.UInt16;
#if BIT32
using size_t = System.UInt32;
#else
using size_t = System.UInt64;
#endif

namespace K4os.Compression.LZ4
{
	internal unsafe class LZ4_64_HC: LZ4_xx
	{
		const int LZ4HC_CLEVEL_MIN = 3;
		const int LZ4HC_CLEVEL_DEFAULT = 9;
		const int LZ4HC_CLEVEL_OPT_MIN = 10;
		const int LZ4HC_CLEVEL_MAX = 12;

		const int LZ4HC_DICTIONARY_LOGSIZE = 16;
		const int LZ4HC_MAXD = (1 <<LZ4HC_DICTIONARY_LOGSIZE);
		const int LZ4HC_MAXD_MASK = (LZ4HC_MAXD - 1);

		const int LZ4HC_HASH_LOG = 15;
		const int LZ4HC_HASHTABLESIZE = (1 << LZ4HC_HASH_LOG);
		const int LZ4HC_HASH_MASK = (LZ4HC_HASHTABLESIZE - 1);

		struct LZ4HC_CCtx_t
		{

			public fixed uint hashTable[LZ4HC_HASHTABLESIZE];
			public fixed ushort chainTable[LZ4HC_MAXD];
			public byte* end;         /* next block here to continue on current prefix */
			public byte* basep;        /* All index relative to this position */
			public byte* dictBase;    /* alternate base for extDict */
			public byte* inputBuffer;       /* deprecated */
			public uint dictLimit;       /* below that point, need extDict */
			public uint lowLimit;        /* below that point, no more dict */
			public uint nextToUpdate;    /* index from which to continue dictionary update */
			public int compressionLevel;
		}
		//LZ4_streamHC_u

		//const int LZ4_STREAMHCSIZE = (4*LZ4HC_HASHTABLESIZE + 2*LZ4HC_MAXD + 56);
		//const int LZ4_STREAMHCSIZE_SIZET = (LZ4_STREAMHCSIZE / sizeof(size_t));

		//union LZ4_streamHC_u
		//{
		//	size_t table [LZ4_STREAMHCSIZE_SIZET];
		//	LZ4HC_CCtx_internal internal_donotuse;
		//};   /* previously typedef'd to LZ4_streamHC_t */


	}
}
