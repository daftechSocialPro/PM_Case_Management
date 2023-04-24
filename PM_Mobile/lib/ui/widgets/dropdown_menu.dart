import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:flutter/material.dart';

class DropDownDemo extends StatelessWidget {


  @override
  Widget build(BuildContext context) {
    return DropdownButton<String>(
      //elevation: 5,
      style: TextStyle(color: Colors.black),
      items: <String>[
        'Quarter 1',
        'Quarter 2',
        'Quarter 3',
        'Quarter 4',
      ].map<DropdownMenuItem<String>>((String value) {
        return DropdownMenuItem<String>(
          value: value,
          child: Text(value),
        );
      }).toList(),
      hint: SubtitleText("Please select a quarter",),
      onChanged: (String? value) {

      },
    );
  }
}