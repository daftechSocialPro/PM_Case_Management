
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:flutter/material.dart';

class DescriptionItem extends StatelessWidget {
  String label;
  String detail;
  DescriptionItem({required this.label , required this.detail, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children:[
          Text("$label",style: CustomStyles.activityTitle(context),),
          SizedBox(height: 8,),
          Text("$detail")
        ],
      ),
    );
  }
}
