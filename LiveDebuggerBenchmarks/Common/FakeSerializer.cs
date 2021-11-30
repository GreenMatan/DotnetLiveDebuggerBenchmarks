﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LiveDebuggerPlayground.Common
{
    public static class FakeSerializer
    {
        public static List<object> Buffer { get; } = new List<object>();

        /// <summary>
        /// Just a fake impl that does nothing. We are not sure yet about the actual implementation so we're just inserting 
        /// items inside arbitrary buffer that does nothing.
        /// </summary>
        /// <param name="items"></param>
        public static void Serialize(params object[] items)
        {
            if (items?.Length == 0)
                return;

            if (Buffer.Count < 1)
            {
                Buffer.Add(items[0]);
                return;
            }

            foreach (var item in items)
            {
                Buffer[0] = item;
            }
        }

        public static void SerializeSingle(object item)
        {
            if (Buffer.Count < 1)
            {
                Buffer.Add(item);
                return;
            }

            Buffer[0] = item;
        }

        public static void Clear() => Buffer.Clear();
    }
}