using Firebase.Database;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;

namespace YogaHybridApp.Views;

public partial class AllClassPage : ContentPage

{
	FirebaseClient fireBaseClient  = new FirebaseClient("https://yoloclassmanagement-default-rtdb.firebaseio.com/");
	private DatabaseConnect database;
	public AllClassPage()
	{
		InitializeComponent();
        //database.LoadTeachersAsync();
        database = new DatabaseConnect();
        LoadTeachers();




    }


    public async void LoadTeachers()
    {
        try
        {
            var teacherList = await database.LoadTeachersAsync();
            listCourses.ItemsSource = teacherList;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }




}