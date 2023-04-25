
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/ui/widgets/error_widget.dart';
import 'package:daf_project1/ui/widgets/task_item.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class TasksPage extends StatelessWidget {

  @override
  Widget build(BuildContext context) {

    return Container(
          padding: EdgeInsets.all(16),
          child: _buildTasks(context)

    );
  }

  Widget _buildTasks(BuildContext context) {
    return Consumer<TasksViewModel>(
      builder: (BuildContext context, value, Widget? child) {
        if(value.tasksUiState == UiState.onError){
          return CustomErrorWidget(onPressed: ()=>{value.getTasks()}, errorMessage: value.errorMessage);
        }
        else if(value.tasksUiState == UiState.onResult){
          List<Task> data = value.tasks;
          return RefreshIndicator(

            onRefresh: (){
              return value.getTasks();
            },
            child: ListView.builder(
              itemCount: data.length,
              itemBuilder: (context, index) {
                Task task = data[index];
                return TaskItem(
                    task: task,
                    onPressed: () =>
                        Navigator.pushNamed(context, '/task_detail', arguments: index));
              },
            ),
          );

        }
        else if(value.tasksUiState == UiState.onLoading){
          return Center(child: CircularProgressIndicator());
        }
        else{
          return Center(
              child: Text("Else branch")
          );
        }
      },

    );
  }

}