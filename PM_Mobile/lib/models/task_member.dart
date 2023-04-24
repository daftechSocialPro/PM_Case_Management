import 'package:daf_project1/models/employee.dart';

class TaskMember{
  late String _id;
  late String _taskId;
  late String _employeeId;
  late Employee _employee;
  
  TaskMember.fromJson(Map<String, dynamic> json):
        _id = json['id'], 
        _taskId = json['taskId'],
        _employeeId = json['employeeId'],
        _employee = Employee.fromJson(json['employee'])
      
  ;
  
}