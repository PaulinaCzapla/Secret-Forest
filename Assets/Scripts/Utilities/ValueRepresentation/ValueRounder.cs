namespace Utilities.ValueRepresentation
{
    /// <summary>
    /// An utility class that rounds values to given value.
    /// </summary>
    public static class ValueRounder
    {
        /// <summary>
        /// Rounds values up to given value.
        /// </summary>
        /// <param name="value"> Value to round. </param>
        /// <param name="roundTo"> Rounding value. </param>
        /// <returns> Rounded value.</returns>
        public static float RoundUp(float value, float roundTo = 1)
        {
            var rest = value % roundTo;
            return (value - rest) + (rest > 0 ? roundTo : 0);
        }
    }
}