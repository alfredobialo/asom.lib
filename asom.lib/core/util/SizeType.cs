namespace asom.lib.core.Util
{
    /// <summary>
    /// Size Type Constants
    /// </summary>
    public enum SizeType : int
    {
        /// <summary>
        /// Size is In Bytes
        /// </summary>
        Bytes,

        /// <summary>
        /// Size Is In KiloBytes
        /// </summary>
        KiloBytes,

        /// <summary>
        /// Size is in MegaBytes
        /// </summary>
        MegaBytes,

        /// <summary>
        /// Size is in GigaBytes
        /// </summary>
        GigaBytes,

        /// <summary>
        /// Size is greater than GigaBytes
        /// </summary>
        LargerThanGigaByte
    };
}