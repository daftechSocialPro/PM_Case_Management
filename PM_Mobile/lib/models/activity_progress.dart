import 'package:daf_project1/models/task_member.dart';

class ActivityProgress{

static final tblActivityProgress = 'ActivityProgress';

static final colId = 'id';
static final colActualBudget = 'actualBudget';
static final colActualWorked ='actualWorked';
static final colDocumentPath='documentPath';
static final colfinanceDocPath= 'financeDocPath';
static final colIsApprovedbyCoordinator = 'isApprovedByCoordinator';
static final colIsApprovedbyFinance = 'isApprovedByFinance';
static final colIsApprovedbyDirector = 'isApprovedByDirector';
static final colCordinatorId = 'coordinatorId';
static final colFinanceId = 'financeId';
static final colFinancRemark = 'financeRemark';
static final colCordinatorRemark = 'cordinatorRemark';
static final colDirectorRemark  = 'directorRemark';
static final colRemark = 'remark' ;
static final colDirectorid = 'directorId';
static final colSubmissionTime= 'submissionTime';
static final colActivityId = 'activityId';
ActivityProgress.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _actualBudget= map[colActualBudget];
  _actualWorked= map[colActualWorked];
  _documentPath= map[colDocumentPath];
  _financeDocPath= map[colfinanceDocPath];
  _isApprovedByCoordinator= map[colIsApprovedbyCoordinator]; 
  _isApprovedByFinance = map[colIsApprovedbyFinance];
  _isApprovedByDirector= map[colIsApprovedbyDirector];
  _coordinatorId=map[colCordinatorId];
  _financeId= map[colFinanceId];
  _financeRemark=map[colFinancRemark];
  _cordinatorRemark= map[colCordinatorRemark];
  _directorRemark= map[colDirectorRemark];
  _remark= map[colRemark];
  _directorId= map[colDirectorid];
  _submissionTime=map[colSubmissionTime];
  _activityId=map[colActivityId];

  }

  late String _id;
  late double _actualBudget;
  late double _actualWorked;
  late String? _documentPath;
  late String? _financeDocPath;
  late int _isApprovedByCoordinator;
  late int _isApprovedByFinance;
  late int _isApprovedByDirector;
  late String? _coordinatorId;
  late String? _financeId;
  late String? _financeRemark;
  late String? _cordinatorRemark;
  late String? _directorRemark;
  late TaskMember _employee;
  late String _remark;
  late String? _directorId;
  late DateTime _submissionTime;
  late String _activityId;

String get activityId => _activityId;
set activityId (String aid ){
_activityId=aid;
}

  late List<ProgressAttachment> _attachments;
set attachments ( List<ProgressAttachment> aid ){
_attachments=aid;
}

  ActivityProgress();

  ActivityProgress.fromJson(Map<String,dynamic> json):
    _id = json['porgressId'],
    _actualBudget = double.parse((json['actualBudget']??0).toString()),
    _actualWorked = double.parse((json['actualWorked']??0).toString()),
    _employee = TaskMember.fromJson(json['submittedBy']),
    _documentPath= json['documentPath'],
    _remark = json['remark'] ?? "Remark goes here...",
        _financeDocPath= json['financeDocumentPath'],
      _isApprovedByCoordinator = json['isApprovedByCoordinator'],
    _isApprovedByFinance = json['isApprovedByFinance'],
  _submissionTime = DateTime.parse(json['sentTime']),
  _isApprovedByDirector = json['isApprovedByDirector'],
  _coordinatorId= json['projectCordinatorId'],
  _financeId = json['financeId'],
  _directorId = json['directorId'],
  _financeRemark= json['financeRemark'],
  _cordinatorRemark= json ['cordinatorRemark'],
  _directorRemark= json ['directorRemark'],

  _attachments = List<ProgressAttachment>.from(json['progressAttacment'].map((e) => ProgressAttachment.fromJson(e)).toList())
  ;


  

  @override
  String toString() {
    return _id;
  }

  int get isApprovedByDirector => _isApprovedByDirector;
  double get actualBudget => _actualBudget;
  String get remark => _remark;
  TaskMember get employee => _employee;
  int get isApprovedByFinance => _isApprovedByFinance;
  int get isApprovedByCoordinator => _isApprovedByCoordinator;
  String? get documentPath => _documentPath;
  double get actualWorked => _actualWorked;
  String? get financeId => _financeId;
  String? get directorId => _directorId;
  String? get financeRemark => _financeRemark;
  String ? get cordinatorRemark => _cordinatorRemark;
  String ? get directorRemark => _directorRemark;
  List<ProgressAttachment> get attachments => _attachments;
  DateTime get submissionTime => _submissionTime;
  String? get financeDocPath => _financeDocPath;
  String get id => _id;
  String? get coordinatorId => _coordinatorId;


  set isApprovedByCoordinator(int value) {
    _isApprovedByCoordinator = value;
  }
  set isApprovedByFinance(int value) {
    _isApprovedByFinance = value;
  }
  set isApprovedByDirector(int value) {
    _isApprovedByDirector = value;
  }
  set financeRemark(String? value) {
    financeRemark = value;
  }
   set cordinatorRemark(String? value) {
    cordinatorRemark = value;
  }
   set directorRemark(String? value) {
    directorRemark = value;
  }



}

class ProgressAttachment{

 static final tblProgressAttachment = 'ProgressAttachment';
 static final colId= 'colId';
 static final colFilePath = 'filePath';
 static final colProgressId = 'progressId';

ProgressAttachment.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
_filePath=map[colFilePath];;
_progressId=map[colProgressId];




}

  late String _id;
  late String _filePath;
  late String _progressId;

  String get progressId => _progressId;
   set progressId(String e){
     _progressId= e;
   }

  String get id => _id;
  String get filePath => _filePath;
  ProgressAttachment.fromJson(Map<String, dynamic> json):
      this._id = json[r"$id"],
      this._filePath = json["filePath"]
  ;

 
}