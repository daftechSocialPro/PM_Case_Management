import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/models/my_activity.dart';
import 'package:daf_project1/ui/widgets/activity_item.dart';
import 'package:daf_project1/ui/widgets/notification_item.dart';
import 'package:daf_project1/viewmodels/activities_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class MyActivitiesPage extends StatelessWidget {
  const MyActivitiesPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    ActivitiesViewModel activitiesViewModel =
        Provider.of<ActivitiesViewModel>(context, listen: false);

    return Scaffold(
        appBar: AppBar(
          title: Text("My Activities"),
        ),
        body: Container(
            padding: EdgeInsets.all(16),
            child: FutureBuilder(
              builder: (context, projectSnap) {
                if (projectSnap.connectionState == ConnectionState.waiting) {
                  print('project snapshot data is: ${projectSnap.data}');
                  return Center(child: CircularProgressIndicator());
                } else if (projectSnap.hasData) {
                  List<MyActivity> data = projectSnap.data as List<MyActivity>;
                  return ListView.builder(
                    itemCount: activitiesViewModel.activities.length,
                    itemBuilder: (context, index) {
                      MyActivity activity = activitiesViewModel.activities[index];
                      return ActivityItem(
                          activity: activity,
                          onPressed: () => Navigator.pushNamed(
                              context, '/activity_detail',
                              arguments: index));
                    },
                  );
                }
                else if(projectSnap.hasError){
                  return Center(child: Text(projectSnap.error.toString()),);
                }
                else {
                  print('project snapshot data is: ${projectSnap.data}');
                  return Center(child: Text("Else Branch"));
                }
              },
              future: activitiesViewModel.getTaskActivities(),
            )));
  }
}
