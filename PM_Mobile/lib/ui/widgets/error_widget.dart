import 'dart:ui';

import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';

class CustomErrorWidget extends StatelessWidget {
  final String errorMessage;
  final VoidCallback onPressed;
  CustomErrorWidget({required this.onPressed, required this.errorMessage, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Icon(Icons.error, size: 76,),
        SizedBox(height: 16,),
        Text(errorMessage, style: CustomStyles.activityTitle(context),
        textAlign: TextAlign.center,
        ),
        SizedBox(height: 8,),
        PrimaryButton(
          onPressed: onPressed,
          buttonText: 'Try Again',
          buttonState: ButtonState.idle,
        )
      ],
    );
  }
}
