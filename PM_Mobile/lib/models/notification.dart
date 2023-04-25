import 'package:daf_project1/models/description.dart';

class Notification{

static final tblNotification= 'Notification';

static final colId='id';
static final colProgressId = 'progressId';
static final colProgramName = 'programName';
static final colPlanName = 'planName';
static final colTaskName= 'taskName';
static final colActivityName = 'activityName';
static final colActivityId = 'activityId';
static final colType ='type';



  late String _id;
  late String _progressId;
  late String _programName;
  late String _planName;
  late String _taskName;
  late String _activityName;
  late String _activityId;
  late int _type;

  
  //Newly submitted progress is waiting for your approval...
  //Please check the newly added progress for {} activity under {} task.
  //Your progress submission is overdue
  // Please submit your progress for {} activity under {} task.
  Notification.fromJson(Map<String,dynamic> json):
    this._id = json[r'$id'],
    this._progressId = json['porgressId'],
    this._programName = json['progamName'],
    this._planName = json['planName'],
    this._taskName = json['taskName'],
    this._activityName = json['activityName'],
    this._activityId = json['activityId'],
    this._type = json['notificationType'] ;

  String get id =>_id;
  String get progressId => _progressId;
  String get programName => _programName;
  String get planName => _planName;
  String get taskName => _taskName;
  String get activityName => _activityName;
  String get activityId => _activityId;
  int get type => _type;
}