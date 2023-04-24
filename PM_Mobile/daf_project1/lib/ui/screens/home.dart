import 'package:daf_project1/ui/screens/notification.dart';
import 'package:daf_project1/ui/screens/tasks.dart';
import 'package:daf_project1/ui/widgets/notification_item.dart';
import 'package:daf_project1/viewmodels/notification_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'my_activities.dart';

class HomePage extends StatefulWidget {
  HomePage({Key? key}) : super(key: key);
  final List<Widget> _widgetOptions = [TasksPage(),MyActivitiesPage(),NotificationPage()];
  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  int _currentIndex = 0;
  @override
  Widget build(BuildContext context) {
    NotificationViewModel notificationViewModel = Provider.of<NotificationViewModel>(context);
    return   Scaffold(
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [
            UserAccountsDrawerHeader(
              accountName: Text("Dr. Besha Moges"),
              accountEmail: Text("Beshamoges@gmail.com"),
              currentAccountPicture: CircleAvatar(
                child: Text(
                  "B",
                  style: TextStyle(fontSize: 40.0),
                ),
              ),
            ),
          ],
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        type: BottomNavigationBarType.fixed,
        items: [
          BottomNavigationBarItem(label: "Coordinating",icon: Icon(Icons.assessment_outlined)),
          BottomNavigationBarItem(label: "Working on",icon: Icon(Icons.work)),
          BottomNavigationBarItem(label: "Notifications", icon: NotificationIcon(counter: notificationViewModel.notifications != null  ? notificationViewModel.getNotificationCount() : -1,)),
        ],
        currentIndex: _currentIndex,
        onTap: (index){
          setState(() {
            _currentIndex = index;
            print("Current index: $_currentIndex");
          });
        },
      ),
      body: widget._widgetOptions.elementAt(_currentIndex),
    );
  }
}