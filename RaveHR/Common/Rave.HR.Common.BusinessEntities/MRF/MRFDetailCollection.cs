//------------------------------------------------------------------------------
//
//  File:           MRFDetailCollection.cs       
//  Author:         Sunil.Mishra
//  Date written:   08/31/2009 01:39:10 PM
//  Description:    Class Contains the Collection of MRF Detail.
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    ---                 ---     -----------
//  08/31/2009 01:39:10 PM   Sunil.Mishra         n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class MRFDetailCollection : CollectionBase
    {
        #region Public Methods

       /// <summary>
       /// 
       /// </summary>
       /// <param name="mrfDetail"></param>
       /// <returns></returns>
        public int Add(object Object)
        {
            return (List.Add(Object));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mrfDetail"></param>
        /// <returns></returns>
        public int IndexOf(object Object)
        {
            return (List.IndexOf(Object));
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="index"></param>
       /// <param name="mrfDetail"></param>
        public void Insert(int index, object Object)
        {
            List.Insert(index, Object);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="mrfDetail"></param>
        public void Remove(object Object)
        {
            List.Remove(Object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public new void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object Item(int index)
        {
            return (object)this.InnerList[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Item(object Object)
        {
            int myIndex;

            myIndex = this.InnerList.IndexOf(Object);

            return (object)this.InnerList[myIndex];
        }
         
        
        #endregion
    }
}
