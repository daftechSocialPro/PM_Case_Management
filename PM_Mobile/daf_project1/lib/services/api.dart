
import 'dart:async';
import 'dart:convert';
import 'dart:io';

import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/models/employee.dart';
import 'package:daf_project1/models/my_activity.dart';
import 'package:daf_project1/models/notification.dart';
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/services/shared_preferences.dart';
import 'package:http/http.dart' as http;

import '../locator.dart';

class Api {



  /*String tasksUrl = "https://qnog9alvyb.api.quickmocker.com/mobileuser/tasks";
  String activitiesUrl = "https://qnog9alvyb.api.quickmocker.com/mobileuser/activities";
  String  notificationUrl = "https://qnog9alvyb.api.quickmocker.com/mobileuser/notifications";*/
  String tasksUrl = "https://run.mocky.io/v3/a6b0c557-2fbe-459d-8185-21b5134b1dfc";
  String activitiesUrl = "https://run.mocky.io/v3/6e6e16c8-5270-45d1-92bd-4e344e1e7987";
  String  notificationUrl = "https://run.mocky.io/v3/51d371bf-6e7a-4c8d-b273-11ce848fead8";

  late Employee _user;

  Employee get user => _user;



  String buildUrl() {
    SharedPreferencesUtil preferencesUtil = locator<SharedPreferencesUtil>();
    String _ipAddress = preferencesUtil.getIpAddress();
    int _port = preferencesUtil.getPortNumber();
    return "$_ipAddress/$_port";

    /*
    * var response = await http.get(url);
  if (response.statusCode == 200) {
    var jsonResponse =
        convert.jsonDecode(response.body) as Map<String, dynamic>;
    var itemCount = jsonResponse['totalItems'];
    print('Number of books about http: $itemCount.');
  } else {
    print('Request failed with status: ${response.statusCode}.');
  }
    * */
  }

  login(String username, String password) {
    //todo return user
    print("Url: ${buildUrl()}.\nUsername: $username\tPassword: $password");
  }

  Future<http.Response> _fetchTasks(String userId) {
    return http.get(Uri.parse(""));
  }

  Future<List<Task>> getTasks()async {
   // try {
      var response = await http.get(Uri.parse(tasksUrl));

      if (response.statusCode == 200) {
        List<Task> tasks = (jsonDecode(response.body) as List).map((e) => Task.fromJson(e)).toList();
        return tasks;

      } else {
        print('Request failed with status: ${response.statusCode}.');
        throw "Request failed";
      }

   /* } on SocketException catch (e) {
       print("No internet connection");
       rethrow;
    }*/

  }
  Future<List<Notification>> getNotifications() async{
    var response = await http.get(Uri.parse(notificationUrl));
    print("getActivities called...");
    if (response.statusCode == 200) {
      print("the response code 200");
      List<Notification> notifications = (jsonDecode(response.body) as List).map((e){
        Notification notification = Notification.fromJson(e);
        return notification;
      }).toList();

      return notifications;

    } else {
      print('Request failed with status: ${response.statusCode}.');
      throw "Request failed";
    }

  }
  Future<List<MyActivity>> getActivities() async{
    var response = await http.get(Uri.parse(activitiesUrl));
    print("getActivities called...");
    if (response.statusCode == 200) {
      print("the response code 200");
      List<MyActivity> activities = (jsonDecode(response.body) as List).map((e){
        MyActivity myActivity = MyActivity.fromJson(e);
        print("MyActivity item : " + myActivity.toString());
        return myActivity;
      }).toList();

      return activities;

    } else {
      print('Request failed with status: ${response.statusCode}.');
      throw "Request failed";
    }
  }
  // id -> ActivityProgress Id
  Future<void> uploadAttachment(id) async{

  }

  Future<void> terminateActivity(String activityId, String employeeId, String remark) async{
    //(Guid ActivityId, Guid EmployeeValueId, string Remark, float lat = 0, float lng = 0)
  }

  //HttpActionResult GetFinalize(Guid ActivityId, float ActualBudget, float ActualWorked,
  // Guid EmployeeValueId, string Remark, float lat = 0, float lng = 0)
  Future<void> finalizeActivity(String activityId, double actualBudget, double actualWorked,
      String employeeId, String remark) async{
  }
  Future<void> addProgress (String activityId, double actualBudget, double actualWorked,
      String employeeId, String remark) async{

  }
  //(Guid periodicPlan, float executedAmount, Guid employeeId, string Remark)
  Future<void> addBSCProgress(String planId, double executedAmount, String employeeId,
      String remark) async{

  }

 /* Future<List<Progress>> getProgresses(String employeeId){

  }

  Future<List<Notification>> getNotifications() async{

  }*/

  Future<void> getBSCPlans() async{
    // using employee id
  }

  //GetProgress(Guid ActivityId,float ActualBudget,float ActualWorked,Guid EmployeeValueId,
// string Remark, float lat=0, float lng=0)

}