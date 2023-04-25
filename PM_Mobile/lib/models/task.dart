import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/conversation.dart';
import 'package:daf_project1/models/task_member.dart';




class Task {

Task();
static final tblTask = 'Task';

static final colId='id';
static final colName = 'description';
static final colTaskId = 'taskId';
static final colProgramName = 'programName';
static final colPlanName= 'planName';
static final colShouldStart= 'shouldStart';
static final colShouldEnd = 'shouldEnd';
static final colActualEnd= 'actualEnd';
static final colActualstart ='actualStart';
static final colPlannedBudget = 'plannedBudget';
static final colActualBudget= 'actualBudget';
static final colGoal = 'goal';
static final colWeight = 'weight';
static final colActualWorked = 'actualWorked';


  late String _id;
  late String _name;
  late String ? _taskId;
  late String _programName;
  late String _planName;
  late DateTime? _shouldStart;
  late DateTime? _actualStart;
  late DateTime? _shouldEnd;
  late DateTime? _actualEnd;
  late double _plannedBudget;
  late double _actualBudget;
  late double _goal;
  late double _weight;
  late double _actualWorked;
  late List<Activity> _assignedActivities;
  late List<Activity> _notAssignedActivities;

 
  late List<TaskMember> _taskMembers;
  late List<Conversation> _conversations;




Task.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _name= map[colName];
  _taskId= map[colTaskId];
  _programName=map[colProgramName];
  _planName= map[colPlanName];
  _shouldStart=map[colShouldStart]!="null" ? DateTime.parse(map[colShouldStart]) : DateTime.now() ;
  _actualStart= map[colActualstart]!="null" ? DateTime.parse(map[colActualstart]) : DateTime.now(); 
  _shouldEnd= map[colShouldEnd]!="null" ? DateTime.parse(map[colShouldEnd]) : DateTime.now()  ;
  _actualEnd = map[colActualEnd]!= "null" ? DateTime.parse(map[colActualEnd]) : DateTime.now();
  _plannedBudget=map[colPlannedBudget];
  _actualBudget=map[colActualBudget];
  _goal=map[colGoal];
  _weight=map[colWeight];
  _actualWorked=map[colActualWorked];




}


  @override
  String toString() {
    return _name;
  }
  // update get tasks
  //https://run.mocky.io/v3/a5ee8b42-4b25-47d2-82b2-906a97da8f63

  Task.fromJson(Map<String, dynamic> json)
      : _id = json['taskId'],
        _programName = json['programName'],
        _planName = json['planName'],
        _name = json['taskName'],
        _shouldStart = json['shouldStart'] != null
            ? DateTime.parse(json['shouldStart'])
            : null,
        _actualStart = json['actualStart'] != null
            ? DateTime.parse(json['actualStart']) : null,
        _shouldEnd = json['shouldEnd'] != null
            ? DateTime.parse(json['shouldEnd'])
            : null,
        _actualEnd = json['actualEnd'] != null
            ? DateTime.parse(json['actualEnd'])
            : null,
        _plannedBudget = double.parse((json['plannedBudget']??0).toString()),
        _actualBudget =double.parse((json['actualBudget']??0).toString()),
        _goal = double.parse((json['taskGoal']??0).toString()),
        _weight = json['taskWeight']??0,
        _actualWorked = double.parse((json['actualWorked']??0).toString()),
        _assignedActivities = List<Activity>.from(json['assignedActivitty'].map((e) => Activity.fromJson(e)).toList()),
        _notAssignedActivities = List<Activity>.from(json['notAssignedActivity'].map((e) => Activity.fromJson(e)).toList()),

      _taskMembers = List<TaskMember>.from(json['taskMember'].map((e) => TaskMember.fromJson((e))).toList()),
      _conversations = List<Conversation>.from(json['taskMemo'].map((e)=>Conversation.fromJson(e))).toList()
  ;

  Map<String, dynamic> toJson() => {};

String get description => _name;
set description (String t ){_name=t;}

String? get taskId => _taskId;
set taskId (String? t ){_taskId=t;}

DateTime? get shouldStart => _shouldStart;
set shouldStart (DateTime? t ){_shouldStart=t;}

  DateTime? get actualStart => _actualStart;
  set actualStart (DateTime? t ){_actualStart=t;}

  DateTime? get shouldEnd => _shouldEnd;
    set shouldEnd (DateTime? t ){_shouldEnd=t;}

  DateTime? get actualEnd => _actualEnd;
    set actualEnd (DateTime? t ){_actualEnd=t;}

  double get plannedBudget => _plannedBudget;
set plannedBudget (double t ){_plannedBudget=t;}

  double get actualBudget => _actualBudget;
  set actualBudget (double t ){_actualBudget=t;}

  String get id => _id;
  double get goal => _goal;
   set goal (double t ){_goal=t;}

  double get weight => _weight;
  set weight (double t ){_weight=t;}

  int get activitiesLength => assignedActivities.length + notAssignedActivities.length;
 
  double get actualWorked => _actualWorked;
  set actualWorked (double t ){_actualWorked=t;}
String get name => _name;


  String get programName => _programName;
  set programName (String t ){_programName=t;}

  String get planName => _planName;
  set planName (String t ){_planName=t;}

  List<TaskMember> get taskMembers => _taskMembers;
  set taskMembers (List<TaskMember> t ){_taskMembers=t;}


  List<Conversation> get conversations => _conversations;
   set conversations (List<Conversation> t ){_conversations=t;}


  List<Activity> get notAssignedActivities => _notAssignedActivities;
  set notAssignedActivities (List<Activity> t ){_notAssignedActivities=t;}

 List<Activity> get assignedActivities => _assignedActivities;
  set assignedActivities (List<Activity> t ){_assignedActivities=t;}

}
