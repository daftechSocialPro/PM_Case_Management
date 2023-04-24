import 'package:daf_project1/locator.dart';
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/ui/widgets/notification_item.dart';
import 'package:daf_project1/ui/widgets/task_item.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class TasksPage extends StatelessWidget {

   TasksPage(){}

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      appBar: AppBar(
        title: Text("My Tasks"),
      ),
      body: Container(
          padding: EdgeInsets.symmetric(vertical: 16),
          child: _buildTasks(context)
      ),
    );
  }

  Widget _buildTasks(BuildContext context) {
    TasksViewModel tasksViewModel = Provider.of<TasksViewModel>(context, listen: false);

    return FutureBuilder(
      builder: (context, projectSnap) {
        print("AsyncSnapshot state : " + projectSnap.connectionState.toString());
        if (projectSnap.connectionState == ConnectionState.waiting) {
          //print('project snapshot data is: ${projectSnap.data}');
          return Center(child: CircularProgressIndicator());
        }
        else if (projectSnap.hasData) {
          List<Task> data = projectSnap.data as List<Task>;
          return ListView.builder(
            itemCount: data.length,
            itemBuilder: (context, index) {
              Task task = data[index];
              return TaskItem(
                  task: task,
                  onPressed: () =>
                      Navigator.pushNamed(context, '/task_activities', arguments: index));
            },
          );
        }
        else if(projectSnap.hasError){
          return Center(child: Text(projectSnap.error.toString()),);
        }
        else {
          return Text("Else");
        }
      },
      future: tasksViewModel.getTasks(),
    );
  }

}