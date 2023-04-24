import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/ui/screens/activity_detail.dart';
import 'package:daf_project1/ui/screens/add_progress.dart';
import 'package:daf_project1/ui/screens/activity_approval.dart';
import 'package:daf_project1/ui/screens/coordinator_activity_detail.dart';
import 'package:daf_project1/ui/screens/home.dart';
import 'package:daf_project1/ui/screens/login.dart';
import 'package:daf_project1/ui/screens/notification.dart';
import 'package:daf_project1/ui/screens/progress_history.dart';
import 'package:daf_project1/ui/screens/task_activities.dart';
import 'package:daf_project1/ui/screens/tasks.dart';
import 'package:daf_project1/ui/screens/terminate_activity.dart';
import 'package:flutter/material.dart';

const String initialRoute = "login";

class CustomRouter {
  static Route<dynamic> generateRoute(RouteSettings settings) {
    switch (settings.name) {
      case '/':
        return MaterialPageRoute(builder: (_) => LoginPage()); //todo determine based on user status
      case '/login':
        return MaterialPageRoute(builder: (_) => LoginPage());
      case '/home':
        return MaterialPageRoute(builder: (_) => HomePage());
      case '/tasks':
        return MaterialPageRoute(builder: (_)=>TasksPage());
      case '/activity_detail':
        var actIndex = settings.arguments as int;
        return MaterialPageRoute(builder: (_) => ActivityDetailPage(activityIndex: actIndex));
      case '/coordinator_activity_detail':
        var arg = settings.arguments as List<int>;
        return MaterialPageRoute(builder: (_) => CoordinatorActivityDetailPage(activityIndex: arg[1], taskIndex: arg[0],));
      case '/activity_approval':
          return MaterialPageRoute(builder: (_)=>ActivityApproval());
      case '/task_activities':
          var taskIndex = settings.arguments as int;
          return MaterialPageRoute(builder: (_)=> TaskActivitiesPage(taskIndex: taskIndex));
      case '/add_progress':
        var arg = settings.arguments;

          return MaterialPageRoute(builder: (_)=> (arg is bool ) ? AddProgressPage(isFinalize: arg,) : AddProgressPage());
      case '/notification':
          return MaterialPageRoute(builder: (_)=>NotificationPage());
      case '/report_history':
          var args = settings.arguments as List<int>;
          return MaterialPageRoute(builder: (_)=>ProgressHistory(activityIndex: args[1], taskIndex: args[0]));
      case '/terminate_activity':
          return  MaterialPageRoute(builder: (_)=>TerminateActivity());
      default:
        return MaterialPageRoute(
            builder: (_) => Scaffold(
              body: Center(
                child: Text('No route defined for ${settings.name}'),
              ),
            ));
    }
  }
}