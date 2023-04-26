import 'dart:async';
import 'dart:io';
import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/employee.dart';
import 'package:daf_project1/models/new_progress.dart';
import 'package:daf_project1/models/notification.dart';
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/services/api_helper.dart';
import 'package:daf_project1/viewmodels/activity_approval_viewmodel.dart';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:path_provider/path_provider.dart';
import 'package:daf_project1/utils/database_helper.dart';
import 'package:daf_project1/models/employee.dart';
import '../locator.dart';
import 'mac_getter.dart';

class Api {
 /* String tasksUrl =
      "https://run.mocky.io/v3/a6b0c557-2fbe-459d-8185-21b5134b1dfc";
  String activitiesUrl =
      "https://run.mocky.io/v3/6e6e16c8-5270-45d1-92bd-4e344e1e7987";
  String notificationUrl =
      "https://run.mocky.io/v3/31648048-049c-466e-ba91-31ce8f809aa9";
  String loginUrl =
      "https://run.mocky.io/v3/cc42b7bb-c0c4-482c-8b51-3dd0d60c2c1a";*/

  Employee? _user;
  late ApiBaseHelper _apiBaseHelper;

  late List<Notification> _notifications;
  List<Activity>? _coordinatingActivities;
  late List<Task> _tasks;

  List<Notification> get notifications => _notifications;

  Employee? get user => _user;


  set user (Employee ? name){
    _user=name;
  }



  Api() {
    _apiBaseHelper = ApiBaseHelper();
  }
  String getRelativeImagePath(){
    return _apiBaseHelper.buildRelativeImagePath();
  }
  Future<Employee?> login(String username, String password) async {
  String? deviceId = locator<MacGetter>().macAddress;
  String relativePath =
      "login?UserName=$username&Password=$password&DeviceId=$deviceId";
  try {
    Map<String, dynamic> jsonMap =
        await _apiBaseHelper.get(relativePath) as Map<String, dynamic>;
    Employee employee = Employee.fromJson(jsonMap);
    employee.username = username;
    employee.password = password;
    employee.userId = employee.id;

    this._user = employee;

    DatabaseHelper? _dbHelper = DatabaseHelper.instanse;
    var users = await _dbHelper.fetchUser();
    if (users.isEmpty) {
      await _dbHelper.insertUser(employee);
    }

    return employee;
  } catch (e) {
    print('Error occurred: $e');
    return null;
  }
}

  logOut() {
    this._user = null;
  }

  Future<List<Task>> getTasks() async {
    String relativePath = "get-tasks?EmployeeId=${_user!.id}"; // todo
    try {
      List<dynamic> jsonMap = await _apiBaseHelper.get(
        relativePath
      );
      List<Task> tasks = ((jsonMap)).map((e) => Task.fromJson(e)).toList();
      this._tasks = tasks;
      return tasks;
    } catch (e, stacktrace) {
      print("${e.toString()}: ${stacktrace.toString()}");
      rethrow;
    }
  }

  Future<List<Notification>> getNotifications() async {
    String relativePath = "get-Notification?empId=${_user!.id}";
    List<dynamic> jsonMap = await _apiBaseHelper.get(relativePath);
    List<Notification> notifications = (jsonMap).map((e) {
      Notification notification = Notification.fromJson(e);
      return notification;
    }).toList();
    _notifications = notifications;
    return notifications;
  }

  Future<List<Activity>> getActivities() async {
    try {
      String relativePath = "get-cordinating-activity?EmployeeId=${_user!.id}";
      List<dynamic> jsonMap =
          await _apiBaseHelper.get(relativePath, isRelative: true);
      List<Activity> activities = (jsonMap).map((e) {
        Activity myActivity = Activity.fromJson(e);
        return myActivity;
      }).toList();
      _coordinatingActivities = activities;
      return activities;
    } catch (e, stacktrace) {
      print("${e.toString()}: ${stacktrace.toString()}");
      rethrow;
    }
  }



  Future<void> terminateActivity(
      String activityId, String employeeId, String remark) async {
    //(Guid ActivityId, Guid EmployeeValueId, string Remark, float lat = 0, float lng = 0)
  }

  //HttpActionResult GetFinalize(Guid ActivityId, float ActualBudget, float ActualWorked,
  // Guid EmployeeValueId, string Remark, float lat = 0, float lng = 0)
  Future<void> finalizeActivity(String activityId, double actualBudget,
      double actualWorked, String employeeId, String remark) async {}

  Future<void> addProgress(String activityId, double actualBudget,
      double actualWorked, String employeeId, String remark) async {}

  //(Guid periodicPlan, float executedAmount, Guid employeeId, string Remark)
  Future<void> addBSCProgress(String planId, double executedAmount,
      String employeeId, String remark) async {}

  List<Activity>? get coordinatingActivities => _coordinatingActivities;

  List<Task> get tasks => _tasks;
 
//GetProgress(Guid ActivityId,float ActualBudget,float ActualWorked,Guid EmployeeValueId,
// string Remark, float lat=0, float lng=0)
  Future<String?> uploadProgress(List<File?> files,File? file, Progress progress) async{
    String relativePath = "Add_ProgressWC?ActivityId=${progress.activityId}"
        "&ActualBudget=${progress.actualBudget}&ActualWorked=${progress.actualWorked}&EmployeeValueId=${progress.employeeId}"
        "&Remark=${progress.remark}"
        "&lat=${progress.lat}&lng=${progress.lng}"
        "&pros=${progress.pros}&quarterId=${progress.quarterId}";
    // if finalize pros => 1 , else pros = 0
    return _apiBaseHelper.uploadMultipleFiles(files,file, relativePath);
  }

  Future<String?> uploadTerminationRequest({required File? file,required String activityId,required String employeeId,required String terminationReason}){

    //http://localhost:15695/mobileuser/Request_Termination?activityId=13c7c502-96af-4468-b256-303001ac8615&employeeId=bbead3e9-fcd4-421f-9979-085c6cb47f09&terminationReason=beka merongal
    String relativePath = "Request_Termination?activityId=$activityId &employeeId=$employeeId &terminationReason=$terminationReason";
    return _apiBaseHelper.uploadMultipleFiles(null,file, relativePath);
  }

  Future<String?> uploadConversation(String parentId, String employeeId, String message,isReplay){
    print("Uploading conversation...");
    //http://192.168.1.10/ipdcs_mobile/mobileuser/Chat?
    // taskId=f8ec3f77-87d4-4b35-a9a3-7fb788c8ae7b
    // &employeeId=ff364af2-5cc5-4bb5-89a6-24e68becbd66&description=alen alen
    //ChatReply?taskMemoId=ed0824ec-e1b9-4343-998e-
    String parentIdField = isReplay ? "ChatReply?taskMemoId" : "Chat?taskId";
    String relativePath = "$parentIdField=$parentId&employeeId=$employeeId&description=$message";
    return _apiBaseHelper.uploadConversation(relativePath);
  }

  Future<String> sendApprovalStatus(String progressId, int isApproved, ApprovalStatus status, String remark) async{
    bool status2 = isApproved==0 ? true:false;
   int s = status==ApprovalStatus.director ?2:status==ApprovalStatus.finance?0:1;
    String relativePath = "Approval_Request?approval_Type=$s&status=$status2&ProgressId=$progressId&remark=$remark";
    return await _apiBaseHelper.uploadApproval(relativePath);
  }


  Future<void> requestDownload(String? _path, String _name) async {
    if(_path != null){
      String _url = _apiBaseHelper.buildRelativeFilePath() + _path;
      //String _url = "https://file-examples-com.github.io/uploads/2017/10/file_example_JPG_1MB.jpg";
      final dir = await getApplicationDocumentsDirectory(); //From path_provider package
      print("Dir $dir");
      var _localPath = dir.path + _name;
      print("Local path : " + _localPath);
      final savedDir = Directory(_localPath);
      await savedDir.create(recursive: true).then((value) async {
        String? _taskid = await FlutterDownloader.enqueue(
          url: _url,
          fileName: _name,
          savedDir: _localPath,
          showNotification: true,
          openFileFromNotification: true,
        );
        if(_taskid != null){
          print(_taskid);
          FlutterDownloader.open(taskId: _taskid);
        }
      });
    }
  }

  String getRelativeFilePath() {
    return _apiBaseHelper.buildRelativeFilePath();
  }
}
