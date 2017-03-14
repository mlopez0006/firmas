class ManejadorFirmas
{
    private static event EventHandler<EventArgs> eventoGuardadoDone;

    public static void MuestraFirmadora(String UrlFirma, int Width, int Heigth)
    {
        SigCtl sigCtl = new SigCtl();
        DynamicCapture dc = new DynamicCaptureClass();
        
        DynamicCaptureResult res = dc.Capture(sigCtl, "Name of the person", "Title", null, null);
        
        if (res == DynamicCaptureResult.DynCaptOK)
        {
            SigObj sigObj = (SigObj)sigCtl.Signature;
            sigObj.set_ExtraData("AdditionalData", "C# test: Additional data");

            try
            {
                // Establecer url de salida de la imagen, el ancho, el alto, formato, e incluso grosor de pluma y color...
                sigObj.RenderBitmap(UrlFirma, Width, Heigth, "image/png", 0.5f, 0x000000, 0xffffff, 10.0f, 10.0f, RBFlags.RenderOutputFilename | RBFlags.RenderColor32BPP | RBFlags.RenderEncodeData);

                SignedDone();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else if (res == DynamicCaptureResult.DynCaptPadError)
        {
            MessageBox.Show("No dispones de una tableta firmadora conectada al sistema.");
        }
        else
        {
            if (res != DynamicCaptureResult.DynCaptCancel)
                MessageBox.Show("Ha ocurrido un error al conectarse a la firmadora.");
        }
    }

    private static void SignedDone()
    {
        if (eventoGuardadoDone != null)
        {
            eventoGuardadoDone(null, new EventArgs());
            eventoGuardadoDone = null;
        }
    }

    public static void SubscribeToSignedDone(EventHandler<EventArgs> metodoEjecutar)
    {
        eventoGuardadoDone -= metodoEjecutar;
        eventoGuardadoDone += metodoEjecutar;
    }

}