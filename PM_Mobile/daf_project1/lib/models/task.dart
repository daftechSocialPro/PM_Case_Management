import 'dart:ffi';

import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/models/task_member.dart';

import 'employee.dart';

class Task {
  late String _id;
  late String _planId;
  late String _description;
  late String _taskId;
  late DateTime? _shouldStart;
  late DateTime? _actualStart;
  late DateTime? _shouldEnd;
  late DateTime? _actualEnd;

  String get id => _id;
  late double _plannedBudget;
  late double _actualBudget;
  late String? _unitOfMeasurement;
  late double _goal;
  late String _weight;
  late double _actualWorked;
  late List<CoordinatorActivity> _activities;
  late List<TaskMember> _taskMembers;

  //todo

  Task();


  @override
  String toString() {
    return _description;
  }

  Task.fromJson(Map<String, dynamic> json)
      : _id = json['taskId'],
        _description = json['taskDescription'],
        _shouldStart = json['shouldStartPeriod'] != null
            ? DateTime.parse(json['shouldStartPeriod'])
            : null,
        _actualStart = json['actuallStart'] != null
            ? DateTime.parse(json['actuallStart']) : null,
        _shouldEnd = json['shouldEnd'] != null
            ? DateTime.parse(json['shouldEnd'])
            : null,
        _actualEnd = json['actuallEnd'] != null
            ? DateTime.parse(json['actuallEnd'])
            : null,
        _plannedBudget = double.parse((json['planedBudget']??0).toString()),
        _actualBudget =double.parse((json['actualBudget']??0).toString()),
        _unitOfMeasurement = json['unitOfMeasurement'],
        _goal = double.parse((json['goal']??0).toString()),
        _weight = double.parse(json['weight']??0).toString(),
        _actualWorked = double.parse((json['actualWorked']??0).toString()),
        _activities = List<CoordinatorActivity>.from(json['activities'].map((e) => CoordinatorActivity.fromJson(e)).toList()),
      _taskMembers = List<TaskMember>.from(json['taskMember'].map((e) => TaskMember.fromJson((e))).toList())

  ;

  Map<String, dynamic> toJson() => {};

  String get planId => _planId;

  String get description => _description;

  String get taskId => _taskId;

  DateTime? get shouldStart => _shouldStart;

  DateTime? get actualStart => _actualStart;

  DateTime? get shouldEnd => _shouldEnd;

  DateTime? get actualEnd => _actualEnd;

  double get plannedBudget => _plannedBudget;

  double get actualBudget => _actualBudget;

  String? get unitOfMeasurement => _unitOfMeasurement;

  double get goal => _goal;

  String get weight => _weight;

  double get actualWorked => _actualWorked;

  List<CoordinatorActivity> get activities => _activities;

  List<TaskMember> get taskMembers => _taskMembers;
}
