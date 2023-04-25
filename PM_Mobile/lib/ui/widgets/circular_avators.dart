import 'package:daf_project1/models/task_member.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:focused_menu/focused_menu.dart';
import 'package:focused_menu/modals.dart';
import 'package:provider/provider.dart';

import 'custom_circle_avatar.dart';

class CustomAvatars extends StatelessWidget {
  late final  List<TaskMember> members;
  CustomAvatars({required this.members});
  
  @override
  Widget build(BuildContext context) {
    String relativeImagePath = Provider.of<TasksViewModel>(context, listen: false).relativeImagePath;
    return _buildMembersPopMenu(context,relativeImagePath);

  }
  Widget _buildMembersPopMenu(BuildContext context, relativeImagePath){
    return FocusedMenuHolder(
        //menuWidth: MediaQuery.of(context).size.width * 0.50,
        blurSize: 0.0,
        //menuItemExtent: 45,
        menuBoxDecoration: BoxDecoration(
            color: Colors.grey,
            borderRadius:
            BorderRadius.all(Radius.circular(15.0))),
        duration: Duration(milliseconds: 100),
        animateMenuItems: false,
        blurBackgroundColor: Colors.black54,
        bottomOffsetHeight: 100,
        openWithTap: true,
        menuItems: List<FocusedMenuItem>.from(members.map((e) => FocusedMenuItem(
            title: Expanded(
              child: Row(
                mainAxisAlignment:
                MainAxisAlignment.spaceBetween,
                children: [
                  CustomCircularAvatar(
                    isSmall: true,
                    imagePath: relativeImagePath+e.imagePath
                  ),
                  Text(e.fullName)
                ],
              ),
            ),
            onPressed: () {}),
        )).toList(),
        onPressed: () {},
        child: _buildSampleMembers(context, relativeImagePath));
  }
  
  Widget _buildSampleMembers(BuildContext context, String relativeImagePath){
    return Container(
      width: MediaQuery.of(context).size.width*0.5,
      height: 40,
      color: Colors.transparent,
      child: Stack(
        children: members.length > 4 ? List<Widget>.from(members.take(4).map((e){
          int index = members.indexOf(e) + 1;
          if(index == 4){
            return  Positioned(
              right: 0,
              //alignment: Alignment.centerRight,
              child: CircleAvatar(
                radius: 20,
                backgroundColor: Colors.white,
                child: CircleAvatar(
                  radius: 18,
                  child: Icon(Icons.more_horiz),),
              ),
            );
          }
          else{
            return Positioned(
              right: index * 18,
              // alignment: Alignment.centerLeft,
              child: CustomCircularAvatar(
                isSmall: true,
                imagePath: relativeImagePath + e.imagePath,
              ),
            );
          }

        })) : List<Widget>.from(members.map((e){
          int index = members.indexOf(e);
          return Positioned(
            right: index*16,
            // alignment: Alignment.centerLeft,
            child: CustomCircularAvatar(
              isSmall: true,
              imagePath:relativeImagePath+e.imagePath),
          );
        } ))
      ),
    );
  }
}
