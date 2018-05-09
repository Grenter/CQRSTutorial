using System;

namespace CQRSTutorial.Cafe.Common
{
    public class TabNotOpen : Exception
    {
    }

    public class DrinksNotOutstanding : Exception
    {
    }

    public class FoodNotOutstanding : Exception
    {
    }

    public class FoodNotPrepared : Exception
    {
    }

    public class NotEnoughPaid : Exception
    {
    }

    public class TabHasUnservedItems : Exception
    {
    }
}
