import 'package:daf_project1/ui/screens/chats.dart';
import 'package:daf_project1/ui/screens/task_activities.dart';
import 'package:daf_project1/viewmodels/task_detail_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../locator.dart';

class TaskDetail extends StatelessWidget {
  int taskIndex;
  TaskDetail({required this.taskIndex,Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return  DefaultTabController(
          length: 3,
          child: Scaffold(
            appBar: AppBar(
              bottom: const TabBar(
                tabs: [
                  Tab(icon: Text("Mine")),
                  Tab(icon: Text("Others")),
                  Tab(icon: Text("Chats")),
                ],
              ),
              title: const Text('Task Activities '),
            ),
            body:  TabBarView(
              children: [
                TaskActivitiesPage(taskIndex: this.taskIndex, isAssigned: true),
                TaskActivitiesPage(taskIndex: this.taskIndex, isAssigned: false),
                Chats(taskIndex: this.taskIndex,),
              ],
            ),
          ),
    );
  }
}
