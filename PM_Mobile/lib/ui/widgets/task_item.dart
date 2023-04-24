import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/ui/widgets/custom_divider.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:daf_project1/utils/date_formatter.dart';
import 'package:flutter/material.dart';

import 'custom_list_tile.dart';

class TaskItem extends StatelessWidget {
  VoidCallback onPressed;
  Task task;
  TaskItem({ required this.task, required this.onPressed, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4),
      child: GestureDetector(
        onTap: this.onPressed,
        child: Card(
            child: Padding(
              padding: const EdgeInsets.symmetric(vertical: 16.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 16.0),
                    child: Text(task.description,style: CustomStyles.taskTitle(context),),
                  ),
                  CustomDivider(),
                  Column(children: [
                    CustomListTile(
                      leading: Icon(Icons.money),
                      title: Text("Planned Budget"),
                    trailing: Text("${task.plannedBudget} Birr"),),
                    CustomListTile(
                      leading: Icon(Icons.height),
                      title: Text("Weight"),
                      trailing: Text("${task.weight}"),
                    ),
                    CustomListTile(
                      leading: Icon(Icons.adjust_rounded),
                      title: Text("Goal"),
                      trailing: Text("${task.goal}"),
                    ),
                  ],),
                  CustomDivider(),
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 16.0),
                    child: Row(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Text("${DateFormatter.formatDate(task.shouldStart)} - ${DateFormatter.formatDate(task.shouldEnd)}",),
                        Text("${task.activities.length} Activities"),
                      ],
                    ),
                  )
                ],
              ),
            ),
        ),
      ),
    );
  }
}
