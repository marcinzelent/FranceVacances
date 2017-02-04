using System;
using France_Vacances.Methods;
using France_Vacances.Model;
using France_Vacances.View;
using France_Vacances.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FVUnitTest
{
    [TestClass]
    public class BookingTest
    {
        private BookingViewModel bookingViewModel = new BookingViewModel();
        private SearchViewModel searchViewModel = new SearchViewModel();
       

        public RelayCommand TestCommand
        {
            get { return searchViewModel.FindAccommodationModelsCommand; }
            set { searchViewModel.FindAccommodationModelsCommand = value; }
        }

        
        public BookingTest()
        {
            searchViewModel.SearchedAccommodationModel = new AccommodationModel
            {
                Region = "Rhone-Alpes"
            };
        }
        [TestMethod]
        public void Test()
        {
            var results = searchViewModel.ResultsCollection;
            Assert.IsNotNull(results);
        }
    }
}
