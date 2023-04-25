
import 'dart:io';

import 'package:path_provider/path_provider.dart';
import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';
import 'package:daf_project1/models/employee.dart';

import 'package:daf_project1/models/task.dart';

import 'package:daf_project1/models/task_member.dart';

import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/activity_progress.dart';

import 'package:daf_project1/models/conversation.dart';
import 'package:daf_project1/models/conversation_replay.dart';






class DatabaseHelper {

  static const _databaseName = 'ToDO.db';
static const _databaseVersion = 1;
DatabaseHelper._();
static final DatabaseHelper instanse = DatabaseHelper._();
Database ? _database;

Future <Database> get database async {

if (_database!=null) return _database !;

_database = await _initDatabase () ; 

return _database!;


}


_initDatabase () async {

Directory dataDirectory = await getApplicationDocumentsDirectory();
String dbpath = join(dataDirectory.path,_databaseName);


return await openDatabase(dbpath,version: _databaseVersion,onCreate: _createDB);

}


_createDB(Database db , int version ) async {


//create employee table
await db.execute('''

CREATE TABLE ${Employee.tblEmployee}(

${Employee.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
${Employee.colUserId} TEXT NOT NULL,
${Employee.colFullName} TEXT NOT NULL,
${Employee.colAmharicFullName} TEXT NOT NULL,
${Employee.colUsernName} TEXT NOT NULL,
${Employee.colPassword} TEXT NOT NULL,
${Employee.colMembershipLevel} TEXT NOT NULL,
${Employee.colStructure} TEXT NOT NULL,
${Employee.colImagePath} TEXT NOT NULL

)
''');

//(get-task)
//task
await db.execute('''

CREATE TABLE  ${Task.tblTask}(

  ${Task.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${Task.colName} TEXT NOT NULL,
  ${Task.colTaskId} TEXT NOT NULL,
  ${Task.colProgramName} TEXT NOT NULL,
  ${Task.colPlanName} TEXT NOT NULL,
  ${Task.colShouldStart} TEXT NOT NULL,
  ${Task.colShouldEnd} TEXT NOT NULL,
  ${Task.colActualEnd} TEXT NOT NULL,
  ${Task.colActualstart} TEXT NOT NULL,
  ${Task.colPlannedBudget} REAL NOT NULL,
  ${Task.colActualBudget} REAL NOT NULL,
  ${Task.colGoal} REAL NOT NULL,
  ${Task.colWeight} REAL NOT NULL,
  ${Task.colActualWorked} REAL NOT NULL
  )
''');
//activities
await db.execute('''

CREATE TABLE  ${Activity.tblActivity}(

  ${Activity.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${Activity.colTaskId} TEXT NOT NULL,
  ${Activity.colDescriprion} TEXT NOT NULL,
  ${Activity.colShouldStart} TEXT NOT NULL,
  ${Activity.colShouldEnd} TEXT NOT NULL,
  ${Activity.colActualStart} TEXT NOT NULL,
  ${Activity.colActualEnd} TEXT NOT NULL,
  ${Activity.colPlannedBudget} REAL NOT NULL,
  ${Activity.colActualBudget} REAL NOT NULL,
  ${Activity.colWeight} REAL NOT NULL,
  ${Activity.colActualWorked} REAL NOT NULL,
  ${Activity.colGoal} REAL NOT NULL,
  ${Activity.colstatus} INTEGER NOT NULL,
  ${Activity.colBegining} REAL NOT NULL,
  ${Activity.colPercentage} REAL NOT NULL,
  ${Activity.colnonandassign} INTEGER NOT NULL
  )
''');
//activityProgres
await db.execute('''

CREATE TABLE  ${ActivityProgress.tblActivityProgress}(

  ${ActivityProgress.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${ActivityProgress.colActualBudget} REAL NOT NULL,
  ${ActivityProgress.colDocumentPath} TEXT NOT NULL,
  ${ActivityProgress.colfinanceDocPath} TEXT NOT NULL,
  ${ActivityProgress.colIsApprovedbyCoordinator} INTEGER NOT NULL,
  ${ActivityProgress.colIsApprovedbyFinance} INTEGER NOT NULL,
  ${ActivityProgress.colIsApprovedbyDirector} INTEGER NOT NULL,
  ${ActivityProgress.colCordinatorId} TEXT NOT NULL,
  ${ActivityProgress.colFinanceId} TEXT NOT NULL,
  ${ActivityProgress.colFinancRemark} TEXT NOT NULL,
  ${ActivityProgress.colCordinatorRemark} TEXT NOT NULL,
  ${ActivityProgress.colDirectorRemark} TEXT NOT NULL,
  ${ActivityProgress.colRemark} TEXT NOT NULL,
  ${ActivityProgress.colDirectorid} TEXT NOT NULL,
  ${ActivityProgress.colSubmissionTime} TEXT NOT NULL,
  ${ActivityProgress.colActivityId} TEXT NOT NULL
  

  )
''');
//targetDivision
await db.execute('''

CREATE TABLE  ${TargetDivision.tblTargetDivision}(

  ${TargetDivision.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${TargetDivision.colTarget} TEXT NOT NULL,
  ${TargetDivision.colActivityId} TEXT NOT NULL

  )
''');
//progressAttachment
await db.execute('''

CREATE TABLE  ${ProgressAttachment.tblProgressAttachment}(

  ${ProgressAttachment.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${ProgressAttachment.colFilePath} TEXT NOT NULL,
  ${ProgressAttachment.colProgressId} TEXT NOT NULL

 
  )
''');
//taskmember
await db.execute('''

CREATE TABLE  ${TaskMember.tblTaskMember}(

  ${TaskMember.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${TaskMember.colEmployeeId} TEXT NOT NULL,
  ${TaskMember.colImagePath} TEXT NOT NULL,
  ${TaskMember.colFullName} TEXT NOT NULL,
  ${TaskMember.colTaskId} TEXT NOT NULL,
  ${TaskMember.colProgId} TEXT NOT NULL
 
  )
''');
//takmemo 
await db.execute('''

CREATE TABLE  ${Conversation.tblConversation}(

  ${Conversation.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${Conversation.colSenderId} TEXT NOT NULL,
  ${Conversation.colSenderName} TEXT NOT NULL,
  ${Conversation.colSenderProfilePath} TEXT NOT NULL,
  ${Conversation.colMessage} TEXT NOT NULL,
  ${Conversation.colSentTime} TEXT NOT NULL,
  ${Conversation.colTaskId} TEXT NOT NULL,
  ${Conversation.colConvid} TEXT NOT NULL
  )
''');

//taskmemo reply 
await db.execute('''

CREATE TABLE  ${Replay.tblConvReply}(

  ${Replay.colId} INTEGER PRIMARY KEY AUTOINCREMENT,
  ${Replay.colSenderId} TEXT NOT NULL,
  ${Replay.colSenderName} TEXT NOT NULL,
  ${Replay.colSenderProfilePath} TEXT NOT NULL,
  ${Replay.colMessage} TEXT NOT NULL,
  ${Replay.colSentTime} TEXT NOT NULL,
  ${Replay.colConversationId} TEXT NOT NULL
  
  )
''');


}


//insert employee
 Future <int> insertUser (Employee employee ) async {


  Database db = await database ; 


 return await  db.rawInsert('INSERT INTO ${Employee.tblEmployee}( ${Employee.colUserId}, ${Employee.colFullName}, ${Employee.colAmharicFullName},${Employee.colImagePath},${Employee.colMembershipLevel},${Employee.colPassword},${Employee.colStructure},${Employee.colUsernName}) VALUES( "${employee.userId}","${employee.fullName}", "${employee.amharicFullName}", "${employee.imagePath}", "${employee.memberShipLevel}", "${employee.password}", "${employee.structure}",  "${employee.username}")');

 }
// fethc employee
 Future<List <Employee>> fetchUser  () async {

Database db = await database;
List<Map> employees = await db.query(Employee.tblEmployee);
return employees.isEmpty?[]: employees.map((e) => Employee.fromMap(e)).toList();
}


//insert , fetch and delete  task

 Future <int> insertTask (Task task ) async {


  Database db = await database ; 

 return await  db.rawInsert('INSERT INTO ${Task.tblTask}( ${Task.colName}, ${Task.colTaskId}, ${Task.colProgramName},${Task.colPlanName},${Task.colShouldStart},${Task.colShouldEnd},${Task.colActualEnd},${Task.colActualstart},${Task.colPlannedBudget},${Task.colActualBudget},${Task.colGoal},${Task.colWeight},${Task.colActualWorked}) VALUES( "${task.description}","${task.id}", "${task.programName}",  "${task.planName}", "${ task.shouldStart.toString() }", "${task.shouldEnd.toString() }",  "${task.actualEnd.toString()}",  "${task.actualStart.toString()}",  ${task.plannedBudget},   ${task.actualBudget},  ${task.goal},  ${task.weight},  ${task.actualWorked})');

 }
 
 Future<List <Task>> fetchTask () async {

  Database db = await database;
  List<Map> taskes = await db.query(Task.tblTask);

  return taskes.isEmpty?[]: taskes.map((e) => Task.fromMap(e)).toList();
}

Future <int> deleteAllTask () async {


  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${Task.tblTask} ''');
}

 
//insert ,fetch and delete all activities
Future <int> insertActivities( Activity act , int actstat)async{

Database db = await database;

 return await  db.rawInsert('INSERT INTO ${Activity.tblActivity}( ${Activity.colTaskId}, ${Activity.colDescriprion}, ${Activity.colShouldStart},${Activity.colShouldEnd},${Activity.colActualStart},${Activity.colActualEnd},${Activity.colPlannedBudget},${Activity.colActualBudget},${Activity.colWeight},${Activity.colActualWorked},${Activity.colGoal},${Activity.colstatus},${Activity.colBegining},${Activity.colPercentage},${Activity.colnonandassign}) VALUES( "${act.taskId}","${act.description}", "${act.shouldStart}", "${act.shouldEnd}", "${act.actuallyStarted}", "${act.actuallyEnded}", "${act.plannedBudget}", "${act.actualBudget}" , "${act.weight}", "${act.actualWorked}", "${act.goal}", "${act.status}", "${act.beginning}", "${act.percentage}", "${actstat}")');


}
Future<List <Activity>> fetchActivities () async {

  Database db = await database;
  List<Map> activities = await db.query(Activity.tblActivity);

  return activities.isEmpty?[]: activities.map((e) => Activity.fromMap(e)).toList();
}
Future <int> deletAllActivities () async {
  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${Activity.tblActivity} ''');
}


//insert , fetch and delete all ActivityProgress
Future <int> insertActivityProgress( ActivityProgress prog)async{

Database db = await database;

 return await  db.rawInsert('INSERT INTO ${ActivityProgress.tblActivityProgress}( ${ActivityProgress.colActualBudget}, ${ActivityProgress.colActualWorked}, ${ActivityProgress.colDocumentPath},${ActivityProgress.colfinanceDocPath},${ActivityProgress.colIsApprovedbyCoordinator},${ActivityProgress.colIsApprovedbyFinance},${ActivityProgress.colIsApprovedbyDirector},${ActivityProgress.colCordinatorId},${ActivityProgress.colFinanceId},${ActivityProgress.colFinancRemark},${ActivityProgress.colCordinatorRemark},${ActivityProgress.colDirectorRemark},${ActivityProgress.colRemark},${ActivityProgress.colDirectorid},${ActivityProgress.colSubmissionTime},${ActivityProgress.colActivityId}) VALUES( ${prog.actualBudget},${prog.actualWorked}, "${prog.documentPath}", "${prog.financeDocPath}", ${prog.isApprovedByCoordinator}, ${prog.isApprovedByFinance},${prog.isApprovedByDirector},"${prog.coordinatorId}","${prog.financeId}","${prog.financeRemark}","${prog.cordinatorRemark}","${prog.directorRemark}","${prog.remark}","${prog.directorId},"${prog.submissionTime}","${prog.activityId}")');


}
Future<List <ActivityProgress>> fetchActivityProgress () async {

  Database db = await database;
  List<Map> progress = await db.query(ActivityProgress.tblActivityProgress);

  return progress.isEmpty?[]: progress.map((e) => ActivityProgress.fromMap(e)).toList();
}
Future <int> deletAllActivitiesProgress () async {
  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${ ActivityProgress.tblActivityProgress} ''');
}


//insert ,fetch and delete all progressAttachment

Future <int> insertProgress (ProgressAttachment progattach) async {


  Database db = await database;
   return await  db.rawInsert('INSERT INTO ${ProgressAttachment.tblProgressAttachment}( ${ProgressAttachment.colFilePath}, ${ProgressAttachment.colProgressId}) VALUES( "${progattach.filePath}","${progattach.progressId}")');

}
Future <List<ProgressAttachment>> fetchProgressAttachments ()async{

  Database db = await database;
  List<Map> attachments = await db.query(ProgressAttachment.tblProgressAttachment);

  return attachments.isEmpty?[]: attachments.map((e) => ProgressAttachment.fromMap(e)).toList();




}
Future <int> deletAllAttachment() async {
  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${ProgressAttachment.tblProgressAttachment} ''');
}

//insert , fetch and delete all targetDivision
Future <int> insertTargetDivision (TargetDivision TD) async {


  Database db = await database;
   return await  db.rawInsert('INSERT INTO ${TargetDivision.tblTargetDivision}( ${TargetDivision.colTarget}, ${TargetDivision.colActivityId}) VALUES( "${TD.target}","${TD.activityId}")');

}
Future <List<TargetDivision>> fetchTargetDivisions ()async{

  Database db = await database;
  List<Map> targetDivisions = await db.query(TargetDivision.tblTargetDivision);

  return targetDivisions.isEmpty?[]: targetDivisions.map((e) => TargetDivision.fromMap(e)).toList();




}
Future <int> deletAllTargetDivision() async {
  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${TargetDivision.tblTargetDivision} ''');
}

//prog taskmember 



 
//insert ,fetch and delete all taskmember
Future <int> insertTaskMember( TaskMember taskmember ,String ? progid)async{

Database db = await database;
if (progid!=null){
return await  db.rawInsert('INSERT INTO ${TaskMember.tblTaskMember}( ${TaskMember.colEmployeeId}, ${TaskMember.colImagePath}, ${TaskMember.colFullName},${TaskMember.colTaskId},${TaskMember.colProgId}) VALUES( "${taskmember.employeeId}","${taskmember.imagePath}", "${taskmember.fullName}",  "","${taskmember.ProgId}")');

}
else{
return await  db.rawInsert('INSERT INTO ${TaskMember.tblTaskMember}( ${TaskMember.colEmployeeId}, ${TaskMember.colImagePath}, ${TaskMember.colFullName},${TaskMember.colTaskId},${TaskMember.colProgId}) VALUES( "${taskmember.employeeId}","${taskmember.imagePath}", "${taskmember.fullName}",  "${taskmember.taskId}","" ) ');

}
 

}
Future<List <TaskMember>> fetchTaskMember () async {

  Database db = await database;
  List<Map> taskmembers = await db.query(TaskMember.tblTaskMember);

  return taskmembers.isEmpty?[]: taskmembers.map((e) => TaskMember.fromMap(e)).toList();
}

Future <int> deleteAllTaskMember () async {


  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${TaskMember.tblTaskMember} ''');
}

//insert ,fetch and derlet all task conv
Future <int> insertconversation( Conversation conv)async{

Database db = await database;

 return await  db.rawInsert('INSERT INTO ${Conversation.tblConversation}( ${Conversation.colSenderId}, ${Conversation.colSenderName}, ${Conversation.colSenderProfilePath},${Conversation.colMessage},${Conversation.colSentTime},${Conversation.colTaskId},${Conversation.colConvid}) VALUES( "${conv.senderId}","${conv.senderName}", "${conv.senderProfilePath}", "${conv.message}", "${conv.sentTime}", "${conv.taskId}", "${conv.convId}")');


}
Future<List <Conversation>> fetchConversation () async {

  Database db = await database;
  List<Map> conversations = await db.query(Conversation.tblConversation);

  return conversations.isEmpty?[]: conversations.map((e) => Conversation.fromMap(e)).toList();
}
Future <int> deletAllConversation () async {
  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${Conversation.tblConversation} ''');
}

//insert ,fetch and derlet all task convReply
Future <int> insertconversationReply( Replay conv)async{

Database db = await database;

 return await  db.rawInsert('INSERT INTO ${Replay.tblConvReply}( ${Replay.colSenderId}, ${Replay.colSenderName}, ${Replay.colSenderProfilePath},${Replay.colMessage},${Replay.colSentTime},${Replay.colConversationId}) VALUES( "${conv.senderId}","${conv.senderName}", "${conv.senderProfilePath}", "${conv.message}", "${conv.sentTime}", "${conv.conversationId}")');


}
Future<List <Replay>> fetchConversationReply () async {

  Database db = await database;
  List<Map> conversationsReply = await db.query(Replay.tblConvReply);

  return conversationsReply.isEmpty?[]: conversationsReply.map((e) => Replay.fromMap(e)).toList();
}
Future <int> deletAllConversationReply () async {
  Database db = await database;
  return await db.rawDelete(''' DELETE FROM ${Replay.tblConvReply} ''');
}


}
