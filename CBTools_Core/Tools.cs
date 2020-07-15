namespace CBTools_Core {
    public static class Tools {
        public static void RefSwap<T>(ref T x, ref T y) {
            T z = x;
            x = y;
            y = z;
        }

        //TODO: Check this works;
        //public static void XORSwap(ref int x, ref int y) {
        //    x ^= y;
        //    y ^= x;
        //    x ^= y;
        //}
    }
}
