
import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/widgets/custom_divider.dart';
import 'package:daf_project1/ui/widgets/custom_list_tile.dart';
import 'package:daf_project1/ui/widgets/description_item.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/ui/widgets/progress_indicator.dart';
import 'package:daf_project1/viewmodels/activities_viewmodel.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class CoordinatorActivityDetailPage extends StatelessWidget {
  int activityIndex;
  int taskIndex;
  bool isCoordinator;
  CoordinatorActivityDetailPage({required this.isCoordinator, required this.activityIndex,required this.taskIndex, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var viewModel = isCoordinator ? Provider.of<ActivitiesViewModel>(context, listen: false) : Provider.of<TasksViewModel>(context, listen:  false);
    Activity activity;
    if(isCoordinator){
      activity = (viewModel as ActivitiesViewModel).activities[activityIndex];

    }
    else{
      activity = (viewModel as TasksViewModel).tasks[taskIndex].notAssignedActivities[activityIndex];

    }
    String detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ipsum consequat nisl vel pretium. Eget egestas purus viverra accumsan in nisl nisi scelerisque. Tellus rutrum tellus pellentesque eu tincidunt tortor.";

    return Scaffold(
      appBar: AppBar(
        title: Text("Activity Detail"),
      ),
      body: Container(
        width: MediaQuery.of(context).size.width,
        padding: EdgeInsets.only(top: 32, left: 16, right: 16, bottom: 16),
        child: SingleChildScrollView(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Center(
                  child: CustomProgressIndicator(
                      label: "Progress So far", percentile: activity.percentage)),
              SizedBox(
                height: 64,
              ),
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.max,
                children: [
                  DescriptionItem(tag: activity.id,label: activity.description,detail: detail,),
                  CustomDivider(),
                  CustomListTile(
                    leading: Icon(Icons.money),
                    title: Text("Planned Budget"),
                    trailing: Text("${activity.plannedBudget}"),),
                  CustomListTile(
                    leading: Icon(Icons.height),
                    title: Text("Weight"),
                    trailing: Text("${activity.weight}%"),
                  ),
                  CustomListTile(
                    leading: Icon(Icons.adjust_rounded),
                    title: Text("Goal"),
                    trailing: Text("${activity.goal}%"),
                  ),
                  CustomDivider(),
                  CustomListTile(title: Text("Status"),
                    trailing: Text("${activity.status}"),
                  ),
                  CustomListTile(title: Text("Received at"),
                    trailing: Text("Unknown"),),
                  // CustomListTile(title: Text("May 26, 2021 - Jan 7, 2021"))
                ],
              ),
              Flexible(
                fit: FlexFit.loose,
                child: Align(
                    alignment: Alignment.bottomCenter,
                    child: PrimaryButton(
                        onPressed: () => {
                          Navigator.pushNamed(context, "/report_history", arguments: activity)
                        },
                        buttonText: "Report history",
                        buttonState: ButtonState.idle)),
              )
            ],
          ),
        ),
      ),
    );
  }

}
