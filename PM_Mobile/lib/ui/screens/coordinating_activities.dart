import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/widgets/activity_item.dart';
import 'package:daf_project1/ui/widgets/error_widget.dart';
import 'package:daf_project1/viewmodels/activities_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class CoordinatingActivities extends StatelessWidget {
  const CoordinatingActivities({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    ActivitiesViewModel activitiesViewModel =
        Provider.of<ActivitiesViewModel>(context, listen: false);

    return  Container(
            padding: EdgeInsets.all(16), child: _buildActivities(context));
  }

  Widget _buildActivities(BuildContext context) {
    //TasksViewModel tasksViewModel = Provider.of<TasksViewModel>(context, listen: false);
    return Consumer<ActivitiesViewModel>(
      builder: (BuildContext context, value, Widget? child) {
        if (value.activitiesUiState == UiState.onError) {
          return CustomErrorWidget(
              onPressed: () => {value.getActivities()},
              errorMessage: value.errorMessage);
        } else if (value.activitiesUiState == UiState.onResult) {
          List<Activity> data = value.activities;
          return RefreshIndicator(
            onRefresh: () {
              return value.getActivities();
            },
            child: ListView.builder(
              itemCount: data.length,
              itemBuilder: (context, index) {
                Activity activity = data[index];
                return ActivityItem(
                    activity: activity,
                    isCoordinating: true,
                    onPressed: () => Navigator.pushNamed(
                        context, '/coordinator_activity_detail',
                        arguments: [0,index,true]));
              },
            ),
          );
        } else if (value.activitiesUiState == UiState.onLoading) {
          return Center(child: CircularProgressIndicator());
        } else {
          return Center(child: Text("Unknown error"));
        }
      },
    );
  }
}
