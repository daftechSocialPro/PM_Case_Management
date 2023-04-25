import 'package:daf_project1/models/activity_progress.dart';
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/models/task_member.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/foundation.dart';

import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/conversation.dart';
import 'package:daf_project1/models/conversation_replay.dart';
import 'package:daf_project1/utils/database_helper.dart';

class TasksViewModel extends ChangeNotifier {
  late Api _api;
  late List<Task> tasks;
  UiState tasksUiState = UiState.initial;
  String errorMessage = "";
  late String relativeImagePath;
  //http://192.168.1.10/ipdcs_mobile/content/EmployeePic/e915dc96-ab13-405a-a8af-339b01168bcb.jpg
  TasksViewModel(Api api) {
    _api = api;
    _initTasks();
  }
  _initTasks() async {
    relativeImagePath = _api.getRelativeImagePath();
    setUiState(UiState.onLoading);
    try {
      tasks = await _api.getTasks();
      DatabaseHelper _dbHelper = DatabaseHelper.instanse;
      var T = await _dbHelper.fetchTask();
      var A = await _dbHelper.fetchActivities();
      var Ap = await _dbHelper.fetchActivityProgress();
      var pAttach = await _dbHelper.fetchProgressAttachments();
      var TD = await _dbHelper.fetchTargetDivisions();
      var TM = await _dbHelper.fetchTaskMember();
      var C = await _dbHelper.fetchConversation();
      var CR = await _dbHelper.fetchConversationReply();



List<ActivityProgress> i= [];
for (var d in Ap){
var j= d;
j.attachments=pAttach.where((x) => x.progressId ==d.id).toList();
i.add(j);
}
List<Activity> u= [];
for (var d in A){
var j= d;
j          .progresses=i.where((x) => x.activityId ==d.id).toList();
u.add(j);
}

      List<Conversation> conv = []; 
       for (var d in C){
          var j= d;
            j.replies=CR.where((x) => x.conversationId==d.convId).toList();

            conv.add(j);

       }
      List<Task> n = [];
      for (var d in T) {
        var t = d;
        t.assignedActivities = u
            .where((x) => x.taskId == d.taskId && x.nonandassign == 1)
            .toList();
        for (var n in t.assignedActivities){

          t.assignedActivities.singleWhere((x) => x.id==n.id).targetDivisions=TD.where((x) => x.activityId==n.id).toList();
        }
        t.notAssignedActivities = u
            .where((x) => x.taskId == d.taskId && x.nonandassign == 0)
            .toList();
   for (var n in t.notAssignedActivities){

          t.notAssignedActivities.singleWhere((x) => x.id==n.id).targetDivisions=TD.where((x) => x.activityId==n.id).toList();
        }
        t.taskMembers = TM.where((x) => x.taskId == d.taskId).toList();
        t.conversations = conv.where((x) => x.taskId == d.taskId).toList();

       
        n.add(t);
      }

      if (T.length != tasks.length || true) {
        _dbHelper.deleteAllTask();
        _dbHelper.deleteAllTaskMember();
        _dbHelper.deletAllConversation();
        _dbHelper.deletAllConversationReply();
        _dbHelper.deletAllActivities();
        _dbHelper.deletAllActivitiesProgress();
        _dbHelper.deletAllAttachment();

        for (var ta in tasks) {
          await _dbHelper.insertTask(ta);

          for (var act in ta.assignedActivities) {
            await _dbHelper.insertActivities(act, 1);
            for (var actProg in act.progresses) {
              actProg.activityId=act.id;
              await _dbHelper.insertActivityProgress(actProg);

              for (var actprogatach in actProg.attachments) {
                actprogatach.progressId=actProg.id;
                await _dbHelper.insertProgress(actprogatach);
                
              }
            }
            for (var actTarget in act.targetDivisions) {
              actTarget.activityId=act.id;
              await _dbHelper.insertTargetDivision(actTarget);
            }
          }
          for (var act2 in ta.notAssignedActivities) {
            await _dbHelper.insertActivities(act2, 0);
            for (var actProg in act2.progresses) {
                 actProg.activityId=act2.id;
              await _dbHelper.insertActivityProgress(actProg);

              for (var actprogatach in actProg.attachments) {
                actprogatach.progressId=actProg.id;
                await _dbHelper.insertProgress(actprogatach);
              }
            }
            for (var actTarget in act2.targetDivisions) {
              actTarget.activityId=act2.id;
              await _dbHelper.insertTargetDivision(actTarget);
            }
          }
          for (var taskmem in ta.taskMembers) {
            taskmem.taskId = ta.id;
            await _dbHelper.insertTaskMember(taskmem, null);
          }
          for (var conv in ta.conversations) {
            conv.taskId = ta.id;
            conv.convId= conv.id;
            await _dbHelper.insertconversation(conv);
            for (var convrep in conv.replies) {
              convrep.conversationId = conv.id;
              await _dbHelper.insertconversationReply(convrep);
            }
          }
        }
      }

      setUiState(UiState.onResult);
    } catch (e) {
      errorMessage = e.toString();
      setUiState(UiState.onError);
    }
  }

  void setUiState(UiState uiState) {
    tasksUiState = uiState;
    notifyListeners();
  }

  Future<void> getTasks() async {
    _initTasks();
  }
}
