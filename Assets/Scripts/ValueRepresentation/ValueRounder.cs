namespace ValueRepresentation
{
    public static class ValueRounder
    {
        public static float RoundUp(float value, float roundTo = 1)
        {
            var rest = value % roundTo;
            return (value - rest) + (rest > 0 ? 1 : 0);
        }
    }
}