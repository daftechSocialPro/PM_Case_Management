import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/widgets/custom_divider.dart';
import 'package:daf_project1/ui/widgets/custom_list_tile.dart';
import 'package:daf_project1/ui/widgets/description_item.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/ui/widgets/progress_indicator.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ActivityDetailPage extends StatelessWidget {
  int activityIndex;
  int taskIndex;
  ActivityDetailPage({required this.taskIndex,required this.activityIndex, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    Activity activity =
        Provider.of<TasksViewModel>(context, listen: false)
            .tasks[taskIndex].assignedActivities[activityIndex];
    String detail =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ipsum consequat nisl vel pretium. Eget egestas purus viverra accumsan in nisl nisi scelerisque. Tellus rutrum tellus pellentesque eu tincidunt tortor.";
    return Scaffold(
      appBar: AppBar(
        title: Text("ActivityDetail"),
        actions: [
          Padding(
              padding: EdgeInsets.only(right: 16.0),
              child: buildPopUpMenuButton(activity)),
        ],
      ),
      body: SingleChildScrollView(
        child: Container(
          width: MediaQuery.of(context).size.width,
          padding: EdgeInsets.symmetric(vertical: 32),
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
                  DescriptionItem(
                    tag: activity.id,
                    label: activity.description,
                    detail: detail,
                  ),
                  CustomDivider(),
                  CustomListTile(
                    leading: Icon(Icons.money),
                    title: Text("Planned Budget"),
                    trailing: Text("${activity.plannedBudget}"),
                  ),
                  CustomListTile(
                    leading: Icon(Icons.height),
                    title: Text("Weight"),
                    trailing: Text("${activity.weight}"),
                  ),
                  CustomListTile(
                    leading: Icon(Icons.adjust_rounded),
                    title: Text("Goal"),
                    trailing: Text("${activity.goal}"),
                  ),
                  CustomDivider(),
                  CustomListTile(
                    title: Text("Status"),
                    trailing: Text("${activity.status}"),
                  ),
                  CustomListTile(
                    title: Text("Received at"),
                    trailing: Text("${activity.beginning.toStringAsFixed(3)}"),
                  ),
                  // CustomListTile(title: Text("May 26, 2021 - Jan 7, 2021"))
                ],
              ),
              //This section of code is replace by floating action button
              Flexible(
                fit: FlexFit.loose,
                child: Align(
                    alignment: Alignment.bottomCenter,
                    child: PrimaryButton(
                        onPressed: () =>
                            {Navigator.pushNamed(context, "/add_progress",  arguments: [activity.id, false, activity.targetDivisions])},
                        buttonText:"Add Progress",
                        buttonState: ButtonState.idle)),
              )
            ],
          ),
        ),
      ),
    );
  }

  Widget buildPopUpMenuButton(Activity activity) {
    return PopupMenuButton<int>(
      itemBuilder: (context) => [
        PopupMenuItem(
          value: 1,
          child: TextButton(
            onPressed: () {
              Navigator.pop(context);
              Navigator.pushNamed(context,'/add_progress',
              arguments: [activity.id, true, activity.targetDivisions]);
            },
            child: Text("Finalize"),
          ),
        ),
        PopupMenuItem(
          value: 2,
          child: TextButton(
            onPressed: () {
              Navigator.pop(context);
              Navigator.pushNamed(context, '/terminate_activity', arguments: activity.id);
            },
            child: Text("Termination Req"),
          ),
        ),
        PopupMenuItem(
          value: 3,
          child: TextButton(
            onPressed: () {
              Navigator.pop(context);
              Navigator.pushNamed(context, "/report_history", arguments: activity);
            },
            child: Text("Report History"),
          ),
        ),
      ],
    );
  }
}
