namespace ExtraExamples
{
    public class Car : Vehicle
    {
        #region Properties
        public string FavouriteSong { get; set; }
        #endregion
        public Car(Make make, string model, string favouriteSong, int wheels = 4)
        {
            Wheels = wheels;
            Make = make;
            Model = model;
        }
    }
}
