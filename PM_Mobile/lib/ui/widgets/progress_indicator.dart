import 'package:flutter/material.dart';
import 'package:percent_indicator/circular_percent_indicator.dart';

class CustomProgressIndicator extends StatelessWidget {
  late final String label;
  late final double percentile;
  CustomProgressIndicator({required this.label, required this.percentile, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    if(percentile > 100){
      percentile = 90; // todo remove this line
    }
    return  new CircularPercentIndicator(
      radius: 120.0,
      lineWidth: 13.0,
      animation: true,
      percent: percentile/100,
      center: new Text(
        "${percentile.toStringAsFixed(1)}%",
        style:
        new TextStyle(fontWeight: FontWeight.bold, fontSize: 20.0),
      ),
      footer: new Text(this.label,
        style:
        new TextStyle(fontWeight: FontWeight.bold, fontSize: 17.0),
      ),
      circularStrokeCap: CircularStrokeCap.round,
      progressColor: Colors.purple,
    );
  }
}
