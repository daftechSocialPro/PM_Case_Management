import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/conversation.dart';
import 'package:daf_project1/models/conversation_replay.dart';
import 'package:daf_project1/models/employee.dart';
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/services/api.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class TaskDetailViewModel extends ChangeNotifier{

  late List<Activity> myActivities;
  late List<Activity> othersActivities;
  late List<Conversation> conversations;
  late Api _api;
  late String userId;
  late Employee user;
  late String fullName;
  late TextEditingController commentTextController;
  late TextEditingController replayTextController;
  late ScrollController commentScrollController;
  late ScrollController replayScrollController;
  late String relativeImagePath;
  late String _taskId;



  TaskDetailViewModel(Api api){
      this._api  = api;
      userId = _api.user!.id;
      user = _api.user!;
      commentTextController = new TextEditingController();
      replayTextController = new TextEditingController();
      commentScrollController = new ScrollController();

  }

  init({required int taskIndex}){
    relativeImagePath = _api.getRelativeImagePath();
   List<Task> tasks = _api.tasks;
   Task currentTask = tasks[taskIndex];
   myActivities = currentTask.assignedActivities;
   othersActivities = currentTask.notAssignedActivities;
   conversations = currentTask.conversations;
   _taskId = currentTask.id;
  }


  addConversation() async{
    String comment = commentTextController.text.trim();
   if(comment.isNotEmpty){
     Conversation newConversation = Conversation(userId,user.fullName,user.imagePath,comment,[],DateTime.now());
     conversations.add(newConversation);
     commentTextController.text = "";
     FocusManager.instance.primaryFocus!.unfocus();
     commentScrollController.animateTo(
       commentScrollController.position.maxScrollExtent,
       curve: Curves.ease,
       duration: const Duration(milliseconds: 100),
     );
     notifyListeners();
     await _api.uploadConversation(_taskId,userId,comment, false);
     print("Add conversation called...");
   }
  }

  addReplay(int conversationIndex) async{
    String replay = replayTextController.text.trim();
    if(replay.isNotEmpty) {
      Replay newReplay = Replay(
          userId, user.fullName, user.imagePath, replay, DateTime.now());
      Conversation parentConversation = conversations[conversationIndex];
      parentConversation.replies.add(newReplay);
      replayTextController.text = "";
      FocusManager.instance.primaryFocus!.unfocus();
      notifyListeners();
      await _api.uploadConversation(
          parentConversation.id, userId, replay, true);
    }

  }
}