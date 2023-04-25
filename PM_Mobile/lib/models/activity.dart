
import 'package:daf_project1/models/description.dart';
import 'package:daf_project1/models/task_member.dart';

import 'activity_progress.dart';
class Activity{
Activity();
static final tblActivity = 'Activity';

static final colId = 'id';
static final colTaskId = 'taskId';
static final colDescriprion = 'description';
static final colShouldStart = 'shouldStart';
static final colShouldEnd = 'shouldEnd';
static final colActualStart = 'actuallyStarted';
static final colActualEnd = 'actuallyEnded';
static final colPlannedBudget = 'plannedBudget';
static final colActualBudget = 'actualBudget';
static final colWeight = 'weight';
static final colActualWorked = '_actualWorked';
static final colGoal = 'goal';
static final colstatus= 'status';
static final colBegining = 'beginning';
static final colPercentage = 'percentage';
static final colnonandassign = 'nonandassign';


Activity.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _taskId= map[colTaskId];
  _description= map[colDescriprion];
  _shouldStart=  map[colShouldStart]!="null" ? DateTime.parse(map[colShouldStart]) : DateTime.now(); 
  _shouldEnd=  map[colShouldEnd]!="null" ? DateTime.parse(map[colShouldEnd]) : DateTime.now(); 
  _actuallyStarted= map[colActualStart]!="null" ? DateTime.parse(map[colActualStart]) : DateTime.now(); 
  _actuallyEnded = map[colActualEnd]!="null" ? DateTime.parse(map[colActualEnd]) : DateTime.now();
 
  _plannedBudget= map[colPlannedBudget];
  _actualBudget= map[colActualBudget];
  _weight= map[colWeight];
  _actualWorked=map[colActualWorked];
  _goal= map[colGoal];

  _status= map[colstatus];
  _beginning= map[colBegining];
  _percentage= map[colPercentage];
  _nonandassign = map[colnonandassign];

  }



  late String _id;
  late String _taskId;
  late String _description;
  late DateTime? _shouldStart;
  late DateTime? _shouldEnd;
  late DateTime? _actuallyStarted;
  late DateTime? _actuallyEnded;
  late double _plannedBudget;
  late double _actualBudget;
  late double _weight;
  late double _actualWorked;
  late double _goal;
  late int _status;
  late double _beginning;
  late double _percentage;
  late int _nonandassign;
  late List<ActivityProgress> _progresses;
  late List<TaskMember> _assignedEmployees;
  late List<TargetDivision> _targetDivisions;


  double get beginning => _beginning;
set beginning (double be ){
  _beginning= be;
}
  int get nonandassign => _nonandassign;
  set nonandassign(int t ){
    _nonandassign=t;
  }
  String get id => _id;
  String get taskId => _taskId;
  set taskId (String taskid) 
      {_taskId=taskid;}

  String get description => _description;
  set description (String des ){_description=des;}
  DateTime? get shouldStart => _shouldStart;
  set shouldStart(DateTime? ss)
  {_shouldStart=ss;}

  DateTime? get shouldEnd => _shouldEnd;

  set shouldEnd(DateTime? ss)
  {_shouldEnd=ss;}

  DateTime? get actuallyStarted => _actuallyStarted;
 set actuallyStarted(DateTime? ss)
  {_actuallyStarted=ss;}

  DateTime? get actuallyEnded => _actuallyEnded;
   set actuallyEnded(DateTime? ss)
  {_actuallyEnded=ss;}

  double get plannedBudget => _plannedBudget;
     set plannedBudget(double ss)
  {_plannedBudget=ss;}

  double get actualBudget => _actualBudget;
       set actualBudget(double ss)
  {_actualBudget=ss;}

  double get weight => _weight;
       set weight(double ss)
  {_weight=ss;}
  double get actualWorked => _actualWorked;
   set actualWorked(double ss)
  {_actualWorked=ss;}
  double get goal => _goal;
   set goal(double ss)
  {_goal=ss;}
  int get status => _status;
   set status(int ss)
  {_status=ss;}
  double get percentage => _percentage;
 set percentage(double ss)
  {_percentage=ss;}

  List<ActivityProgress> get progresses => _progresses;
  List<TaskMember> get assignedEmployees => _assignedEmployees;
  List<TargetDivision> get targetDivisions => _targetDivisions;


set progresses(List<ActivityProgress> ss)
  {_progresses=ss;}

set targetDivisions(List<TargetDivision> ss)
  {_targetDivisions=ss;}

  Activity.fromJson(Map<String,dynamic> json):
      this._id = json['activityId'],
      this._taskId = json['taskId'],
      this._description = json['activityName'] ?? "Activity Name", //todo include this field in the api response
      this._shouldStart = json['shouldEnd'] != null ? DateTime.parse(json['shouldEnd'])
          : null,
      this._shouldEnd = json['shouldEnd'] != null
          ? DateTime.parse(json['shouldEnd'])
          : null,

        _actuallyStarted = json['actualStart'] != null
            ? DateTime.parse(json['actualStart'])
            : null,
        _actuallyEnded = json['actualEnd'] != null
            ? DateTime.parse(json['actualEnd'])
            : null,
        _plannedBudget = json['plannedBudget'],
        _actualBudget = json['actualBudget'] ?? 0,
        _actualWorked = json['actualWorked'],
        _status = json['status'],
        _weight = json['activityWeight'],
        _beginning = json['begining'],
        _goal = json['goal'],
        _percentage = json['percentage'],
        _progresses = List<ActivityProgress>.from(json['progress'].map((e)=>ActivityProgress.fromJson(e)).toList()),
        _targetDivisions = json['targetDivisionVM'] != null ?  List<TargetDivision>.from(json['targetDivisionVM'].map((e)=>TargetDivision.fromJson(e)).toList()) : [],
      _assignedEmployees = List<TaskMember>.from(json['assignedEmployee'].map((e)=>TaskMember.fromJson(e)).toList())

  ;


}


class TargetDivision{
 
 static final tblTargetDivision = 'targetDivision';
 static final colId = 'id';
 static final colTarget = 'target';
 static final colActivityId = 'activityId';
TargetDivision.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _target=map[colTarget];
  _activityId=map[colActivityId];
}
  late String _id;
  late String _target;
  late String _activityId;
  
  String get id => _id;
  String get target => _target;
  String get activityId => _activityId;
  set activityId (String d){
    _activityId=d;
  }
  TargetDivision.fromJson(Map<String,dynamic> json):
      this._id = json['targetDivisionId'],
      this._target = json['target']
      ;

  
}