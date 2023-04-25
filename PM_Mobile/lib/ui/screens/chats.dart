import 'package:daf_project1/models/conversation.dart' as conversationModel;
import 'package:daf_project1/ui/widgets/chat_composer.dart';
import 'package:daf_project1/ui/widgets/conversation.dart';
import 'package:daf_project1/viewmodels/task_detail_viewmodel.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class Chats extends StatelessWidget {
  int taskIndex;
  Chats({required this.taskIndex, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    TaskDetailViewModel taskDetailViewModel = Provider.of<TaskDetailViewModel>(context, listen: false);
    return Padding(
      padding: const EdgeInsets.fromLTRB(16.0,16.0,16.0,0),
      
      child: Column(
        children: [
          Expanded(
            child:
            Consumer<TaskDetailViewModel>(
              builder: (context,value,_){
                return ListView.builder(
                  controller: value.commentScrollController,
                  itemCount: value.conversations.length,
                  itemBuilder: (BuildContext context, int index) {
                    conversationModel.Conversation currentConversation = value.conversations[index];
                    return Conversation(conversation: currentConversation,conversationIndex: index,);
                  },);
              },
            ),
          ),
          ChatComposer(onPressed: ()=>{
            taskDetailViewModel.addConversation()
          },textEditingController: taskDetailViewModel.commentTextController )
        ],
      ),
    );
  }
}
