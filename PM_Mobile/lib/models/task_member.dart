import 'package:daf_project1/models/employee.dart';

class TaskMember {
  TaskMember();
  static final tblTaskMember = 'taskMember';

  static final colId = 'id';
  static final colEmployeeId = 'employeeId';
  static final colImagePath = 'imagePath';
  static final colFullName = 'fullName';
  static final colTaskId = 'taskId';
  static final colProgId = 'ProgId';

  late String _id;
  late String _employeeId;
  late String _imagePath;
  late String _fullName;
  late String _taskId;
  late String _progId;


TaskMember.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _employeeId= map[colEmployeeId];
  _imagePath= map[colImagePath];
  _fullName= map[colFullName];
  _taskId= map[colTaskId];
    _progId= map[colProgId];
  }

  TaskMember.fromJson(Map<String, dynamic> json)
      : _id = json[r'$id'],
        _employeeId = json['employeeId'],
        _imagePath = json['imagePath'],
        _fullName = json['employeeName'];

  String get taskId => _taskId;
  set taskId(String taskid) {
    _taskId = taskid;
  }
   String get ProgId => _progId;
  set ProgId(String progd) {
    _progId = progd;
  }


  String get employeeId => _employeeId;
  set employeeId(String emp) {
    _employeeId = emp;
  }

  String get imagePath => _imagePath;
  set imagePath(String img) {
    _imagePath = img;
  }

  String get fullName => _fullName;
  set fullName(String name) {
    _fullName = name;
  }
}
