import 'package:flutter/material.dart';
import 'package:percent_indicator/linear_percent_indicator.dart';

class CustomLinearProgressIndicator extends StatelessWidget {
  double percentile;
  CustomLinearProgressIndicator({required this.percentile, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return new LinearPercentIndicator(
      width: 140.0,
      lineHeight: 18.0,
      percent: this.percentile/100,
      center: Text(
        "${percentile.toStringAsFixed(1)}%",
        style: new TextStyle(fontSize: 12.0),
      ),
      linearStrokeCap: LinearStrokeCap.roundAll,
      progressColor: Colors.purple,
    );
  }
}
