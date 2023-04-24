import 'package:daf_project1/models/activity_progress.dart';
import 'package:daf_project1/ui/widgets/activity_progress_item.dart';
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ProgressHistory extends StatelessWidget {
  int taskIndex;
  int activityIndex;

  ProgressHistory({required this.taskIndex, required this.activityIndex, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    TasksViewModel tasksViewModel = Provider.of<TasksViewModel>(context);
    List<ActivityProgress> progresses = tasksViewModel.tasks[taskIndex].activities[activityIndex].progress;

    return Scaffold(
      appBar: AppBar(
        title: Text("Reports"),
      ),
      body: Container(
    child: progresses.length > 0 ? ListView.builder(
    itemCount: progresses.length,
        itemBuilder: (context,index){
          return ActivityProgressItem(progress: progresses[index],);

        }) : Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16),
          child: Center(child: Text("There is no progress added to this activity.",
          textAlign: TextAlign.center,),),
        ),
    )
    );
  }
}
