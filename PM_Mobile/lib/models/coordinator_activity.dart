import 'package:daf_project1/models/iactivity.dart';

import 'activity_progress.dart';
import 'employee.dart';

class CoordinatorActivity extends Activity {
  late String _id;
  late String _taskId;
  late String _description;
  late DateTime? _shouldStart;
  late DateTime? _shouldEnd;

  String get id => _id;
  late DateTime? _actuallyStarted;
  late DateTime? _actuallyEnded;
  late double _plannedBudget;
  late double _actualBudget;
  late double _weight;
  late double _actualWorked;
  late double _goal;
  late String _employeeId;
  late Employee _employee;
  late int _status; // Map this status to meaning full description
  late List<ActivityProgress> _progress;

  CoordinatorActivity.fromJson(Map<String, dynamic> json)
      : _id = json['id'],
        _taskId = json['taskId'],
        _description = json['activityDescription'],
        _shouldStart = json['shouldStat'] != null
            ? DateTime.parse(json['shouldStat'])
            : null,
        _shouldEnd = json['shouldEnd'] != null
            ? DateTime.parse(json['shouldEnd'])
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
        _employee = Employee.fromJson(json['employee']),
        _weight = json['weight'],
        _status = json['status'],
        _goal = json['goal'],
        _actualWorked = double.parse((json['actualWorked']??0).toString()),
        _progress = List<ActivityProgress>.from(json['progress'].map((e)=>ActivityProgress.fromJson(e)).toList())
  ;
  Map<String, dynamic> toJson() => {};

  @override
  String toString() {
    return _description;
  }

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

  String get employeeId => _employeeId;

  Employee get employee => _employee;

  int get status => _status;

  List<ActivityProgress> get progress => _progress;
}


enum ActivityStatus { finalized, terminated, onProgress }
