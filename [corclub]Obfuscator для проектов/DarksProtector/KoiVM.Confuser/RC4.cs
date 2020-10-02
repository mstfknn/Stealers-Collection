namespace KoiVM.Confuser
{
    internal class RC4
    {
        // Adopted from BouncyCastle

        private static readonly int STATE_LENGTH = 256;

        private readonly byte[] engineState;
        private byte[] workingKey;
        private int x;
        private int y;

        public RC4(byte[] key)
        {
            this.workingKey = (byte[]) key.Clone();

            this.x = 0;
            this.y = 0;

            if(this.engineState == null) this.engineState = new byte[STATE_LENGTH];

            // reset the state of the engine
            for(int i = 0; i < STATE_LENGTH; i++) this.engineState[i] = (byte) i;

            int i1 = 0;
            int i2 = 0;

            for(int i = 0; i < STATE_LENGTH; i++)
            {
                i2 = ((key[i1] & 0xff) + this.engineState[i] + i2) & 0xff;
                // do the byte-swap inline
                byte tmp = this.engineState[i];
                this.engineState[i] = this.engineState[i2];
                this.engineState[i2] = tmp;
                i1 = (i1 + 1) % key.Length;
            }
        }

        public void Crypt(byte[] buf, int offset, int len)
        {
            for(int i = 0; i < len; i++)
            {
                this.x = (this.x + 1) & 0xff;
                this.y = (this.engineState[this.x] + this.y) & 0xff;

                // swap
                byte tmp = this.engineState[this.x];
                this.engineState[this.x] = this.engineState[this.y];
                this.engineState[this.y] = tmp;

                // xor
                buf[i + offset] = (byte) (buf[i + offset]
                                          ^ this.engineState[(this.engineState[this.x] + this.engineState[this.y]) & 0xff]);
            }
        }
    }
}