import 'package:daf_project1/ui/screens/notifications.dart';
import 'package:daf_project1/ui/screens/tasks.dart';
import 'package:daf_project1/ui/widgets/custom_circle_avatar.dart';
import 'package:daf_project1/ui/widgets/custom_divider.dart';
import 'package:daf_project1/ui/widgets/notification_icon.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:daf_project1/viewmodels/home_viewmodel.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/viewmodels/notification_viewmodel.dart';
import 'package:daf_project1/viewmodels/user_auth.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'coordinating_activities.dart';

class HomePage extends StatefulWidget {
  HomePage({Key? key}) : super(key: key);
  final List<Widget> _widgetOptions = [TasksPage(),CoordinatingActivities(),NotificationPage()];
  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  int _currentIndex = 0;
  String title = "My Tasks";
  @override
  Widget build(BuildContext context) {
    HomeViewModel homeViewModel = Provider.of<HomeViewModel>(context, listen: false);
    return   Scaffold(
      appBar: AppBar(
        title: Text(title),
        backgroundColor: Colors.blue[300],
      ),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [
            Container(
              margin: const EdgeInsets.only(bottom: 8.0),
              child: DrawerHeader(
                decoration: BoxDecoration(
                  color: Colors.blue[300],
                ),
                child: Column(
                  children: [
                    SizedBox.fromSize(
                        size: Size.square(72),
                        child:  CustomCircularAvatar(
                          isSmall: false,
                          imagePath:   homeViewModel.imagePath ,
                        )

                        //CircleAvatar(child: Text("B", style: TextStyle(fontSize: 40.0),))
              ),
                    SizedBox(height: 16,),
                    Text(homeViewModel.user!.fullName, style: Theme.of(context).textTheme.subtitle1!.copyWith(
                      color: Colors.white
                    )),
                    SizedBox(height: 4,),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          homeViewModel.user!.memberShipLevel ?? "Membership isn't set",
                          overflow: TextOverflow.ellipsis,
                          style: TextStyle(
                          color: Colors.white
                        ),), SizedBox(width: 8,),Text("|", style: CustomStyles.activityTitle(context).copyWith(
                          color: Colors.white
                        ),),SizedBox(width: 8,),
                        Text(homeViewModel.user!.structure??"Structure isn't set",
                            overflow: TextOverflow.ellipsis,
                            style: TextStyle(
                            color: Colors.white
                        ))
                      ],)
                  ],
                ),
              ),
            ),
            _buildDrawerActionItems(context,isSelected: _currentIndex == 0,onPressed: () =>{
              setState(() {
                _currentIndex = 0;
                Navigator.pop(context);
              })
            },
              label: "My Tasks",
              icon: Icons.work_outline_outlined,),
            _buildDrawerActionItems(context,isSelected: _currentIndex == 1, onPressed: () =>{
              setState(() {
                _currentIndex = 1;
                Navigator.pop(context);
              })
            },//
              label: "Activities Coordinating",
              icon: Icons.assessment_outlined,),
            _buildDrawerActionItems(context,isSelected: _currentIndex == 2,onPressed: () =>{
              setState(() {
                _currentIndex = 2;
                Navigator.pop(context);
              })
            },
              label: "Data Managment",
              icon: Icons.assessment_outlined,),
            _buildDrawerActionItems(context,isSelected: _currentIndex == 2,onPressed: () =>{
              setState(() {
                _currentIndex = 2;
                Navigator.pop(context);
              })
            },
              label: "Notifications",
              icon: Icons.notifications_outlined,),
            SizedBox(height: 8,),
            CustomDivider(),
            _buildDrawerActionItems(context,onPressed: (){
              final UserAuthentication userAuth = Provider.of<UserAuthentication>(context, listen: false);
              userAuth.isAuthenticated = false;
            },
            label: "Sign Out",
            icon: Icons.logout_outlined,),
          ],
        ),
      ),
      bottomNavigationBar: BottomNavigationBar(
        type: BottomNavigationBarType.fixed,
        items: [
          BottomNavigationBarItem(label: "My Tasks",icon: Icon(Icons.work_outline_outlined)),
          BottomNavigationBarItem(label: "Coordinating",icon: Icon(Icons.assessment_outlined)),
          BottomNavigationBarItem(label: "Notifications", icon: Consumer<NotificationViewModel>(
              builder: (context,value,_){
            return NotificationIcon(counter: value.notificationCount);
          }))
        ],
        currentIndex: _currentIndex,
        onTap: (index){
          setState(() {
            _currentIndex = index;
            switch(index){
              case 0:
                title = "My Tasks";
                break;
              case 1:
                title = "Activities";
                break;
              case 2:
                  title = "Notifications";
                  break;
            }
          });
        },
      ),
      body: widget._widgetOptions.elementAt(_currentIndex),
    );
  }
  Widget _buildDrawerActionItems(BuildContext context, {isSelected = false, required String label, required VoidCallback onPressed,required IconData icon,}){
    var theme = Theme.of(context);
    return  Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16.0,vertical: 8),
      child: Row(children: [
        Icon(icon,
        color: isSelected ? theme.primaryColor : null,
        ),
        SizedBox(width: 16,),
        GestureDetector(
            onTap: onPressed,
            child: Text(label, style: isSelected ?theme.textTheme.button!.copyWith(
              color: theme.primaryColor
            ) : theme.textTheme.button!.copyWith(
              fontWeight: FontWeight.normal
            ),)),
      ],),
    );

  }
}