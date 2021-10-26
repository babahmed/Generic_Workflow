import 'package:flutter/material.dart';
import 'package:nb_utils/nb_utils.dart';
import 'package:prokit_flutter/fullApps/walletApp/component/WAVoucherComponent.dart';
import 'package:prokit_flutter/fullApps/walletApp/model/WalletAppModel.dart';
import 'package:prokit_flutter/fullApps/walletApp/utils/WADataGenerator.dart';

class WAVoucherScreen extends StatefulWidget {
  static String tag = '/WAVoucherScreen';

  @override
  WAVoucherScreenState createState() => WAVoucherScreenState();
}

class WAVoucherScreenState extends State<WAVoucherScreen> {
  List<WAVoucherModel> voucherList = waVouchersList();

  @override
  void initState() {
    super.initState();
    init();
  }

  Future<void> init() async {
    //
  }

  @override
  void setState(fn) {
    if (mounted) super.setState(fn);
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        extendBodyBehindAppBar: true,
        backgroundColor: Colors.transparent,
        appBar: AppBar(
          backgroundColor: Colors.transparent,
          title: Text(
            'Vouchers',
            style: boldTextStyle(color: Colors.black, size: 20),
          ),
          leading: Container(
            margin: EdgeInsets.all(8),
            decoration: boxDecorationWithRoundedCorners(
              backgroundColor: Colors.white,
              borderRadius: BorderRadius.circular(12),
              border: Border.all(color: Colors.grey.withOpacity(0.2)),
            ),
            child: Icon(Icons.arrow_back),
          ).onTap(() {
            finish(context);
          }),
          centerTitle: true,
          elevation: 0.0,
          brightness: Brightness.dark,
        ),
        body: Container(
          height: context.height(),
          width: context.width(),
          padding: EdgeInsets.only(top: 40),
          decoration: BoxDecoration(image: DecorationImage(image: AssetImage('images/walletApp/wa_bg.jpg'), fit: BoxFit.cover)),
          child: Container(
            margin: EdgeInsets.only(top: 30),
            decoration: boxDecorationRoundedWithShadow(
              16,
              backgroundColor: Colors.white,
            ),
            child: SingleChildScrollView(
              child: Wrap(
                spacing: 16,
                runSpacing: 16,
                alignment: WrapAlignment.center,
                children: voucherList.map((item) {
                  return WAVoucherComponent(voucherModel: item);
                }).toList(),
              ).paddingAll(16),
            ),
          ),
        ),
      ),
    );
  }
}
