import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class CustomStyles{

  static TextStyle taskTitle(BuildContext context){
      return Theme.of(context).textTheme.headline6!.copyWith(
        fontWeight: FontWeight.w400
      );
  }
  static TextStyle activityTitle(BuildContext context){
    return Theme.of(context).textTheme.subtitle1!.copyWith(
        fontSize: 18,
        fontWeight: FontWeight.w500
    );
  }
}