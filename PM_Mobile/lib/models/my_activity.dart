import 'package:daf_project1/models/iactivity.dart';

import 'activity_progress.dart';
import 'employee.dart';

class MyActivity extends Activity{
  late String _id;
  late String _taskId;
  late String _description;
  late DateTime? _shouldStart;
  late DateTime? _shouldEnd;
  late int _activityType;
  String get id => _id;
  late DateTime? _actuallyStarted;
  late DateTime? _actuallyEnded;
  late double _plannedBudget;
  late double _actualBudget;
  late double _weight;
  late double _actualWorked;
  late double _goal;
  late String? _employeeId;
  late int _status; //todo
  late double _progress;

  //todo
  MyActivity();

  @override
  toString(){
    return "$_description";
  }

  MyActivity.fromJson(Map<String, dynamic> json)
      : _id = json['id'],
        _taskId = json['taskId'],
        _description = json['activityDescription'],
        _activityType = json['activityType'],
        _shouldStart = json['shouldStat'] != null
            ? DateTime.parse(json['shouldStat'])
            : null,
        _shouldEnd = json['shouldEndDate'] != null
            ? DateTime.parse(json['shouldEndDate'])
            : null,
        _plannedBudget = double.parse((json['planedBudget']??0).toString()),
        _actuallyStarted = json['actualStart'] != null
            ? DateTime.parse(json['actualStart'])
            : null,
        _actuallyEnded = json['actualEnd'] != null
            ? DateTime.parse(json['actualEnd'])
            : null,
        _actualBudget = double.parse((json['actualBudget']??0).toString()),
        _employeeId = json['employeeId'],
        _weight = json['weight'],
        _status = json['status'],
        _goal = json['goal'],
        _actualWorked = double.parse((json['actualWorked']??0).toString()),
        _progress = double.parse((json['progress']??0).toString())

  ;
  Map<String, dynamic> toJson() => {};


  String get taskId => _taskId;

  String get description => _description;

  DateTime? get shouldStart => _shouldStart;

  DateTime? get shouldEnd => _shouldEnd;

  DateTime? get actuallyStarted => _actuallyStarted;

  DateTime? get actuallyEnded => _actuallyEnded;

  double get plannedBudget => _plannedBudget;

  double get actualBudget => _actualBudget;

  double get weight => _weight;

  double get actualWorked => _actualWorked;

  double get goal => _goal;

  String? get employeeId => _employeeId;


  int get status => _status;

  double get progress => _progress;
}


enum ActivityStatus { finalized, terminated, onProgress }
