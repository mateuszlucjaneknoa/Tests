namespace Test1.Examples
{
    public static class Examples
    {
        public static string GetTimeOfDay(DateTime dateTime)
        {
            if (dateTime.Hour >= 0 && dateTime.Hour < 6)
            {
                return "Night";
            }
            if (dateTime.Hour >= 6 && dateTime.Hour < 12)
            {
                return "Morning";
            }
            if (dateTime.Hour >= 12 && dateTime.Hour < 18)
            {
                return "Noon";
            }
            return "Evening";
        }

        //private DateTime DateTimeFromPesel(string pesel)
        //{
        //    int[] numberAsIntArray = new int[11];
        //    for (int i = 0; i < 11; i++)
        //    {
        //        numberAsIntArray[i] = Character.getNumericValue(super.getNumber().charAt(i));
        //    }

        //    int birthDay = 10 * numberAsIntArray[4] + numberAsIntArray[5];
        //    int birthYear = 10 * numberAsIntArray[0] + numberAsIntArray[1];
        //    int birthMonth = 10 * numberAsIntArray[2] + numberAsIntArray[3];
        //    if (birthDay > 31) super.setCorrect(false);

        //    if (birthMonth <= 12) birthYear += 1900;
        //    else if (birthMonth <= 32)
        //    {
        //        birthYear += 2000;
        //        birthMonth -= 20;
        //    }
        //    else if (birthMonth <= 52)
        //    {
        //        birthYear += 2100;
        //        birthMonth -= 40;
        //    }
        //    else if (birthMonth <= 72)
        //    {
        //        birthYear += 2200;
        //        birthMonth -= 60;
        //    }
        //    else if (birthMonth <= 92)
        //    {
        //        birthYear += 1800;
        //        birthMonth -= 80;
        //    }
        //    else super.setCorrect(false);

        //    return LocalDate.of(birthYear, birthMonth, birthDay);
        //}
    }
}