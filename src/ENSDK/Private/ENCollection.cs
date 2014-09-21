using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;

namespace ENSDK
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class ENCollection : _Collection
    {
        private Microsoft.VisualBasic.Collection m_Collection = new Microsoft.VisualBasic.Collection();

        public void Add(ref object Item, ref object Key, ref object Before)
        {
            object tempVar = null;
            Add(ref Item, ref Key, ref Before, ref tempVar);
        }

        public void Add(ref object Item, ref object Key)
        {
            object tempVar = null;
            object tempVar2 = null;
            Add(ref Item, ref Key, ref tempVar, ref tempVar2);
        }

        public void Add(ref object Item)
        {
            object tempVar = null;
            object tempVar2 = null;
            object tempVar3 = null;
            Add(ref Item, ref tempVar, ref tempVar2, ref tempVar3);
        }

        public void Add(ref object Item, ref object Key, ref object Before, ref object After)
        {

            string sKey = null;
            if (!(Key is System.Reflection.Missing) && Key != null)
            {
                sKey = Key.ToString();
            }

            object oBefore = null;
            if (IsNumeric(Before))
            {
                oBefore = Convert.ToInt32(Before);
            }
            else if (!(Before is System.Reflection.Missing) && Before != null)
            {
                oBefore = Before.ToString();
            }

            object oAfter = null;
            if (IsNumeric(After))
            {
                oAfter = Convert.ToInt32(After);
            }
            else if (!(After is System.Reflection.Missing) && After != null)
            {
                oAfter = After.ToString();
            }

            m_Collection.Add(Item, sKey, oBefore, oAfter);

        }

        public int Count()
        {

            return m_Collection.Count;

        }

        public object Item(ref object Index)
        {

            if (IsNumeric(Index))
            {
                return m_Collection[Convert.ToInt32(Index)];
            }
            else if (m_Collection.Contains(Index.ToString()))
            {
                return m_Collection[Index.ToString()];
            }
            else
            {
                throw new Exception(String.Format("Item '{0}' not in collection.", Index));
            }

        }

        public void Remove(ref object Index)
        {

            if (IsNumeric(Index))
            {
                m_Collection.Remove(Convert.ToInt32(Index));
            }
            else
            {
                m_Collection.Remove(Index.ToString());
            }

        }

        [System.Runtime.CompilerServices.IndexerName("MyItem")]
        public object this[object Index]
        {
            get
            {
                return Item(ref Index);
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return m_Collection.GetEnumerator();
        }

        private static bool IsNumeric(object expression)
        {
            if (expression == null)
                return false;

            double testDouble;
            if (double.TryParse(expression.ToString(), out testDouble))
                return true;

            //VB's 'IsNumeric' returns true for any boolean value:
            bool testBool;
            if (bool.TryParse(expression.ToString(), out testBool))
                return true;

            return false;
        }

    }

    [ComImport(), TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FDispatchable | TypeLibTypeFlags.FHidden), Guid("A4C46780-499F-101B-BB78-00AA00383CBB"), DefaultMember("Item")]
    public interface _Collection : IEnumerable
    {
        [return: MarshalAs(UnmanagedType.Struct)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0)]
        object Item([In(), MarshalAs(UnmanagedType.Struct)] ref object Index);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(1)]
        void Add([In(), MarshalAs(UnmanagedType.Struct)] ref object Item, [In(), MarshalAs(UnmanagedType.Struct)] ref object Key, [In(), MarshalAs(UnmanagedType.Struct)] ref object Before);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(1)]
        void Add([In(), MarshalAs(UnmanagedType.Struct)] ref object Item, [In(), MarshalAs(UnmanagedType.Struct)] ref object Key);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(1)]
        void Add([In(), MarshalAs(UnmanagedType.Struct)] ref object Item);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(1)]
        void Add([In(), MarshalAs(UnmanagedType.Struct)] ref object Item, [In(), MarshalAs(UnmanagedType.Struct)] ref object Key, [In(), MarshalAs(UnmanagedType.Struct)] ref object Before, [In(), MarshalAs(UnmanagedType.Struct)] ref object After);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(2)]
        int Count();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(3)]
        void Remove([In(), MarshalAs(UnmanagedType.Struct)] ref object Index);
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "", MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler), MarshalCookie = "")]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(-4)]
        new IEnumerator GetEnumerator();
    }

}



////using System.Collections.Generic;
////using System;
////using System.Threading.Tasks;
////using System.Diagnostics;
////using System.Data;
////using System.Xml.Linq;
////using Microsoft.VisualBasic;
////using System.Collections;
////using System.Linq;
////// End of VB project level imports

////using System.Runtime.InteropServices;
////using System.Runtime.InteropServices.CustomMarshalers;
////using System.Reflection;
////using System.Runtime.CompilerServices;


////namespace ENSDK
////{
////    public class COMCollection : _Collection
////    {

////        Microsoft.VisualBasic.Collection m_Collection = new Microsoft.VisualBasic.Collection();

////        public void Add(ref object Item, ref object Key, ref object Before, ref object After)
////        {

////            string sKey = null;
////            if (!(Key is System.Reflection.Missing) && Key != null)
////            {
////                sKey = Key.ToString();
////            }

////            object oBefore = null;
////            if (Information.IsNumeric(Before))
////            {
////                oBefore = System.Convert.ToInt32(Before);
////            }
////            else if (!(Before is System.Reflection.Missing) && Before != null)
////            {
////                oBefore = Before.ToString();
////            }

////            object oAfter = null;
////            if (Information.IsNumeric(After))
////            {
////                oAfter = System.Convert.ToInt32(After);
////            }
////            else if (!(After is System.Reflection.Missing) && After != null)
////            {
////                oAfter = After.ToString();
////            }

////            m_Collection.Add(Item, sKey, oBefore, oAfter);

////        }

////        public int Count()
////        {

////            return m_Collection.Count;

////        }

////        public dynamic Item(ref object Index)
////        {

////            if (Information.IsNumeric(Index))
////            {
////                return m_Collection[System.Convert.ToInt32(Index)];
////            }
////            else if (m_Collection.Contains(Index.ToString()))
////            {
////                return m_Collection[Index.ToString()];
////            }
////            else
////            {
////                Information.Err().Raise(Number: 5, Description: "Item \'" + Index.ToString() + "\' not in collection.");
////                return Conversion.ErrorToString(5);
////            }

////        }

////        public void Remove(ref object Index)
////        {

////            if (Information.IsNumeric(Index))
////            {
////                m_Collection.Remove(System.Convert.ToInt32(Index));
////            }
////            else
////            {
////                m_Collection.Remove(Index.ToString());
////            }

////        }

////        //public dynamic this[object Index]
////        //{
////        //get
////        //{
////        //return Item(ref Index);
////        //}
////        //}

////        public System.Collections.IEnumerator GetEnumerator()
////        {
////            return m_Collection.GetEnumerator();
////        }

////    }


////    [ComImport(), TypeLibType(TypeLibTypeFlags.FDual & TypeLibTypeFlags.FDispatchable & TypeLibTypeFlags.FHidden), Guid("A4C46780-499F-101B-BB78-00AA00383CBB"), DefaultMember("Item")]
////    public interface _Collection : IEnumerable
////    {
////        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0)]
////        [return: MarshalAs(UnmanagedType.Struct)]
////        dynamic Item([In(), MarshalAs(UnmanagedType.Struct)]ref object Index);
////        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(1)]
////        void Add([In(), MarshalAs(UnmanagedType.Struct)]ref object Item, [In(), MarshalAs(UnmanagedType.Struct)]ref object Key, [In(), MarshalAs(UnmanagedType.Struct)]ref object Before, [In(), MarshalAs(UnmanagedType.Struct)]ref object After);
////        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(2)]
////        int Count();
////        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(3)]
////        void Remove([In(), MarshalAs(UnmanagedType.Struct)]ref object Index);
////        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(-4)]
////        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "", MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler), MarshalCookie = "")]
////        new IEnumerator GetEnumerator();
////    }


////}


//using System;
//using System.Collections;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//namespace ENSDK
//{
//    //I decide not to use UCOMIEnumVariant since I got no idea how
//    //to Marshal int back to array. As a result, I re-define the
//    //IEnumVARIANT interface to simplified my work
//    [
//            Guid("00020404-0000-0000-C000-000000000046"),
//            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
//    ]
//    public interface IEnumVARIANT
//    {
//        [MethodImpl(MethodImplOptions.PreserveSig)]
//        int Next(UInt32 celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0), Out]object[] rgelt, IntPtr pceltFetched);
//        void Skip(UInt32 celt);
//        void Reset();
//        void Clone(int ppenum);
//    }

//    [Guid("A4C46780-499F-101B-BB78-00AA00383CBB")]
//    public interface IVBCollection
//    {
//        [DispId(0)]
//        object Item([In]ref object Index);

//        [DispId(1)]
//        void Add([In]ref object Item, [In, Optional]ref object Key, [In, Optional]ref object Before, [In, Optional]ref object After);

//        [DispId(2)]
//        Int32 Count();

//        [DispId(3)]
//        void Remove([In]ref object Index);

//        [DispId(-4)]
//        [return: MarshalAs(UnmanagedType.IUnknown)]
//        object _NewEnum();
//    }

//    [
//            Guid("A4C4671C-499F-101B-BB78-00AA00383CBB"),
//            ClassInterface(ClassInterfaceType.None)
//    ]
//    public class COMCollection : IVBCollection, IEnumVARIANT, IEnumerable
//    {
//        ICollection m_collection;
//        IEnumerator m_enumerator;

//        internal COMCollection(ICollection c)
//        {
//            m_collection = c;
//            m_enumerator = c.GetEnumerator();
//        }

//        #region Implementation of IEnumerable
//        public IEnumerator GetEnumerator()
//        {
//            return m_enumerator;
//        }
//        #endregion

//        #region Implementation of IVBCollection
//        public object Item(ref object Index)
//        {
//            throw new NotSupportedException("Method Item() not supported for VbEnumableCollection.");
//        }
//        public void Add(ref object Item, ref object Key, ref object Before, ref object After)
//        {
//            throw new NotSupportedException("Method Add() not supported for VbEnumableCollection.");
//        }
//        public Int32 Count()
//        {
//            return m_collection.Count;
//        }
//        public void Remove(ref object Index)
//        {
//            throw new NotSupportedException("Method Remove() not supported for VbEnumableCollection.");
//        }
//        public object _NewEnum()
//        {
//            return new COMCollection(m_collection);
//        }
//        #endregion

//        #region Implementation of IEnumVariant
//        public int Next(UInt32 celt, object[] rgelt, IntPtr pceltFetched)
//        {
//            if (pceltFetched != IntPtr.Zero)
//                Marshal.WriteInt32(pceltFetched, 0);
//            if (celt > 1)
//                throw new NotSupportedException("Each time can fetch one item in VbCallableCollection.");
//            if (m_enumerator.MoveNext())
//            {
//                rgelt[0] = m_enumerator.Current;
//                if (pceltFetched != IntPtr.Zero)
//                    Marshal.WriteInt32(pceltFetched, 1);
//                return 0; //S_OK
//            }
//            else
//                return 1; //S_FALSE
//        }
//        public void Skip(UInt32 celt)
//        {
//            throw new NotSupportedException("Method Skip() not supported in VbEnumerableCollection.");
//        }
//        public void Reset()
//        {
//            m_enumerator.Reset();
//        }
//        public void Clone(int ppenum)
//        {
//            throw new NotSupportedException("Cannot clone interface.");
//        }
//        #endregion
//    }
//}

////using System;
////using System.Collections.Generic;
////using System.Text;
////using System.Runtime.InteropServices;

////namespace ENSDK
////{

////    [Guid("0490E147-F2D2-4909-A4B8-3533D2F264D0")]
////    [ComVisible(true)]
////    public interface IMyCollectionInterface
////    {

////        int Add(object value);
////        void Clear();
////        bool Contains(object value);
////        int IndexOf(object value);
////        void Insert(int index, object value);
////        void Remove(object value);
////        void RemoveAt(int index);

////        [DispId(-4)]
////        System.Collections.IEnumerator GetEnumerator();

////        [DispId(0)]
////        [System.Runtime.CompilerServices.IndexerName("_Default")]
////        object this[int index]
////        {
////            get;
////        }
////    }


////    [ComVisible(true)]
////    [ClassInterface(ClassInterfaceType.None)]
////    [ComDefaultInterface(typeof(IMyCollectionInterface))]
////    [ProgId("CollectionsInterop.VB6InteropArrayList")]
////    public class COMCollection : System.Collections.ArrayList, IMyCollectionInterface
////    {

////        #region IMyCollectionInterface Members


////        // COM friendly strong typed GetEnumerator

////        [DispId(-4)]
////        public System.Collections.IEnumerator GetEnumerator()
////        {
////            return base.GetEnumerator();
////        }



////        #endregion
////    }
////}


