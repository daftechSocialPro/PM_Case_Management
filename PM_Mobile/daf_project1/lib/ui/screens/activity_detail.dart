import 'package:daf_project1/models/iactivity.dart';
import 'package:daf_project1/models/my_activity.dart';
import 'package:daf_project1/ui/widgets/custom_divider.dart';
import 'package:daf_project1/ui/widgets/custom_list_tile.dart';
import 'package:daf_project1/ui/widgets/description_item.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/ui/widgets/progress_indicator.dart';
import 'package:daf_project1/viewmodels/activities_viewmodel.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ActivityDetailPage extends StatelessWidget {
  int activityIndex;
  ActivityDetailPage({required this.activityIndex, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    MyActivity activity =
        Provider.of<ActivitiesViewModel>(context, listen: false)
            .activities[activityIndex];
    String detail =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ipsum consequat nisl vel pretium. Eget egestas purus viverra accumsan in nisl nisi scelerisque. Tellus rutrum tellus pellentesque eu tincidunt tortor.";
    return Scaffold(
      appBar: AppBar(
        title: Text("ActivityDetail"),
        actions: [
          Padding(
              padding: EdgeInsets.only(right: 16.0),
              child: buildPopUpMenuButton()),
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
                      label: "Progress So far", percentile: activity.progress)),
              SizedBox(
                height: 64,
              ),
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.max,
                children: [
                  DescriptionItem(
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
                    trailing: Text("81%"),
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
                            {Navigator.pushNamed(context, "/add_progress")},
                        buttonText:"Add Progress",
                        buttonState: ButtonState.idle)),
              )
            ],
          ),
        ),
      ),
    );
  }

  Widget buildPopUpMenuButton() {
    return PopupMenuButton<int>(
      itemBuilder: (context) => [
        PopupMenuItem(
          value: 1,
          child: TextButton(
            onPressed: () {
              Navigator.pushNamed(context,'/add_progress',
              arguments: true);
            },
            child: Text("Finalize"),
          ),
        ),
        PopupMenuItem(
          value: 2,
          child: TextButton(
            onPressed: () {
              Navigator.pushNamed(context, '/terminate_activity');
            },
            child: Text("Terminate"),
          ),
        ),
        PopupMenuItem(
          value: 3,
          child: TextButton(
            onPressed: () {
              Navigator.pop(context);
            },
            child: Text("Report History"),
          ),
        ),
      ],
    );
  }
}
