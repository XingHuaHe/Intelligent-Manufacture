using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_XRF_TecsondeSync
{
    class CheckUtils
    {
        public static void CalcularLength(byte[] Data)
        {
            if (Data.Length < 8) return;

            int datelegth = Data.Length - 8;
            //if (datelegth > 65565) throw new Exception("数据长度错误");
            byte[] rbyte = IntToBytes(datelegth);
            Data[4] = rbyte[1];
            Data[5] = rbyte[0];
        }
        public static byte[] IntToBytes(int value)
        {
            byte[] b = new byte[4];
            b[0] = (byte)(value);
            b[1] = (byte)(value >> 8);
            b[2] = (byte)(value >> 16);
            b[3] = (byte)(value >> 24);
            return b;
        }
        public static int ByteToInt(byte[] inbytes)
        {
            if (inbytes.Length > 4) return 0;
            int value = 0;
            for (int i = 0; i < inbytes.Length; i++)
                value += (inbytes[i] & 0xFF) << (i * 8);
            return value;
        }
        public static bool CheckSum(byte[] Data, Boolean TypeJudge)
        {
            try
            {
                //Log.v("Click", "数据采集到："+Convert.PrintHexString(Data));
                int idxBuffer = 0;
                long CS = 0;
                int PktLen = 0;
                Byte CHKSUM_MSB;
                Byte CHKSUM_LSB;

                PktLen = ((Data[4] & 0xFF) * 256) + (Data[5] & 0xFF);//第四位&0xFF * 256 + 第五位&0xFF（高位在前低位在后）
                //Log.v("Click", "数据采集到PktLen："+PktLen);
                CS = (Data[0] & 0xFF) + (Data[1] & 0xFF) + (Data[2] & 0xFF) + (Data[3] & 0xFF) + (Data[4] & 0xFF) + (Data[5] & 0xFF);//前六位相加
                //Log.v("Click", "数据采集到："+CS);
                if (PktLen > 0)
                {
                    for (idxBuffer = 0; idxBuffer < PktLen; idxBuffer++)
                    {
                        CS = CS + (Data[idxBuffer + 6] & 0xFF);//从第七位开始相加到PktLen长度（PktLen表示需要校验的位数，从上面获取）
                        //Log.v("Click", "数据采集到："+CS);
                    }
                }
                CS = (CS ^ 0xFFFF) + 1;//把总和 ^ 0xFFFF 再加一  ^异或
                //Log.v("Click", "数据采集到："+CS);
                CHKSUM_MSB = (byte)((CS & 0xFF00) / 256);	// 把上面得到的CS& 0xFF00 在除以256
                //Log.v("Click", "数据采集到："+CS);
                CHKSUM_LSB = (byte)(CS & 0xFF);//把上面得到的CS & 0xFF
                // Log.v("Click", "数据采集到："+CS);

                //Log.w("CHECKSUM","CHKSUM_MSB:\t"+((CS & 0xFF00) / 256)+"\tCHKSUM_LSB:\t"+(CS & 0xFF)+"\tData[PktLen + 6]:\t"+(Data[PktLen + 6]&0xFF)+"\tData[PktLen + 7]:\t"+(Data[PktLen + 7]&0xFF));
                if (TypeJudge == true)
                {
                    //判断第PktLen + 6位&0xFF 与 CHKSUM_MSB &0xFF是否相等  并且 第PktLen + 7位&0xFF 与CHKSUM_LSB & 0xFF是否相等，如果都相等  校验通过
                    if (((Data[PktLen + 6] & 0xFF) == (CHKSUM_MSB & 0xFF)) && ((Data[PktLen + 7] & 0xFF) == (CHKSUM_LSB & 0xFF)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Log.w("CHECKSUM","CHKSUM_MSB:\t"+CHKSUM_MSB+"CHKSUM_LSB:\t"+CHKSUM_LSB);
                    Data[Data.Length - 2] = CHKSUM_MSB;
                    Data[Data.Length - 1] = CHKSUM_LSB;
                }
                return true;
            }
            catch (Exception)
            {
                
            }
            return true;
        }
    }
}
