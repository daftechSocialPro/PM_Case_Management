
import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/ui/widgets/activity_item.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class TaskActivitiesPage extends StatelessWidget {
  int taskIndex;
  TaskActivitiesPage({required this.taskIndex, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    TasksViewModel tasksViewModel = Provider.of<TasksViewModel>(context, listen: false);
    return Scaffold(
      appBar: AppBar(
        title: Text("Activities"),
      ),
      body:Container(
        padding: EdgeInsets.all(16),
        child:  ListView.builder(
        itemCount: tasksViewModel.tasks[taskIndex].activities.length,
        itemBuilder: (context, index) {
          CoordinatorActivity activity = tasksViewModel.tasks[taskIndex].activities[index];
          return ActivityItem(
              activity: activity,
              onPressed: () =>
                  Navigator.pushNamed(context, '/coordinator_activity_detail', arguments: [taskIndex,index]));
        },
      ),
    )
    );
  }
}
