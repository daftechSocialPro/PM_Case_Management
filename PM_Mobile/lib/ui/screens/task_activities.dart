
import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/widgets/activity_item.dart';
import 'package:daf_project1/viewmodels/task_detail_viewmodel.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class TaskActivitiesPage extends StatelessWidget {
  int taskIndex;
  bool isAssigned;
  TaskActivitiesPage({required this.taskIndex,required this.isAssigned, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    TaskDetailViewModel taskDetailViewModel = Provider.of<TaskDetailViewModel>(context, listen: false);
    taskDetailViewModel.init(taskIndex: taskIndex);
    return Container(
        padding: EdgeInsets.all(16),
        child:
        isAssigned && taskDetailViewModel.myActivities.length == 0 ? Center(
          child: Text("You have no activities assigned in the current task.", textAlign: TextAlign.center,),
        ) : !isAssigned && taskDetailViewModel.othersActivities.length == 0 ? Center(
    child: Text("Activities aren't assigned for other members in the current task.", textAlign: TextAlign.center,)) :
        ListView.builder(
        itemCount: isAssigned ?  taskDetailViewModel.myActivities.length :taskDetailViewModel.othersActivities.length,
        itemBuilder: (context, index) {
          Activity activity = isAssigned ? taskDetailViewModel.myActivities[index] :taskDetailViewModel.othersActivities[index] ;
          return ActivityItem(
              activity: activity,
              onPressed: (){
                String route = "";
                List args = [];
                if(isAssigned){
                  route = '/activity_detail';
                  args = [taskIndex,index];
                }else{
                  route = '/coordinator_activity_detail';
                  args = [taskIndex,index,false];
                }
                Navigator.pushNamed(context,route, arguments: args);
              }
                 );
        },
      ),);
  }
}
