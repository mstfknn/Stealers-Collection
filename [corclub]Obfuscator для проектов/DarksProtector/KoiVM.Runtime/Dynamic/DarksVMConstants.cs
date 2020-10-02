#pragma warning disable 0649

namespace KoiVM.Runtime.Dynamic
{
    internal static class DarksVMConstants
    {
        public static byte REG_R0, REG_R1, REG_R2, REG_R3, REG_R4, REG_R5, REG_R6, REG_R7, REG_BP, REG_SP, REG_IP, REG_FL, REG_K1, REG_K2, REG_M1, REG_M2,
        FL_OVERFLOW, FL_CARRY, FL_ZERO, FL_SIGN, FL_UNSIGNED, FL_BEHAV1, FL_BEHAV2, FL_BEHAV3, OP_NOP;

        public static byte OP_LIND_PTR;
        public static byte OP_LIND_OBJECT;
        public static byte OP_LIND_BYTE;
        public static byte OP_LIND_WORD;
        public static byte OP_LIND_DWORD;
        public static byte OP_LIND_QWORD;
        public static byte OP_SIND_PTR;
        public static byte OP_SIND_OBJECT;
        public static byte OP_SIND_BYTE;
        public static byte OP_SIND_WORD;
        public static byte OP_SIND_DWORD;
        public static byte OP_SIND_QWORD;
        public static byte OP_POP;
        public static byte OP_PUSHR_OBJECT;
        public static byte OP_PUSHR_BYTE;
        public static byte OP_PUSHR_WORD;
        public static byte OP_PUSHR_DWORD;
        public static byte OP_PUSHR_QWORD;
        public static byte OP_PUSHI_DWORD;
        public static byte OP_PUSHI_QWORD;
        public static byte OP_SX_BYTE;
        public static byte OP_SX_WORD;
        public static byte OP_SX_DWORD;
        public static byte OP_CALL;
        public static byte OP_RET;
        public static byte OP_NOR_DWORD;
        public static byte OP_NOR_QWORD;
        public static byte OP_CMP;
        public static byte OP_CMP_DWORD;
        public static byte OP_CMP_QWORD;
        public static byte OP_CMP_R32;
        public static byte OP_CMP_R64;
        public static byte OP_JZ;
        public static byte OP_JNZ;
        public static byte OP_JMP;
        public static byte OP_SWT;
        public static byte OP_ADD_DWORD;
        public static byte OP_ADD_QWORD;
        public static byte OP_ADD_R32;
        public static byte OP_ADD_R64;
        public static byte OP_SUB_R32;
        public static byte OP_SUB_R64;
        public static byte OP_MUL_DWORD;
        public static byte OP_MUL_QWORD;
        public static byte OP_MUL_R32;
        public static byte OP_MUL_R64;
        public static byte OP_DIV_DWORD;
        public static byte OP_DIV_QWORD;
        public static byte OP_DIV_R32;
        public static byte OP_DIV_R64;
        public static byte OP_REM_DWORD;
        public static byte OP_REM_QWORD;
        public static byte OP_REM_R32;
        public static byte OP_REM_R64;
        public static byte OP_SHR_DWORD;
        public static byte OP_SHR_QWORD;
        public static byte OP_SHL_DWORD;
        public static byte OP_SHL_QWORD;
        public static byte OP_FCONV_R32_R64;
        public static byte OP_FCONV_R64_R32;
        public static byte OP_FCONV_R32;
        public static byte OP_FCONV_R64;
        public static byte OP_ICONV_PTR;
        public static byte OP_ICONV_R64;
        public static byte OP_VCALL;
        public static byte OP_TRY;
        public static byte OP_LEAVE;

        public static byte VCALL_EXIT;
        public static byte VCALL_BREAK;
        public static byte VCALL_ECALL;
        public static byte VCALL_CAST;
        public static byte VCALL_CKFINITE;
        public static byte VCALL_CKOVERFLOW;
        public static byte VCALL_RANGECHK;
        public static byte VCALL_INITOBJ;
        public static byte VCALL_LDFLD;
        public static byte VCALL_LDFTN;
        public static byte VCALL_TOKEN;
        public static byte VCALL_THROW;
        public static byte VCALL_SIZEOF;
        public static byte VCALL_STFLD;
        public static byte VCALL_BOX;
        public static byte VCALL_UNBOX;
        public static byte VCALL_LOCALLOC;

        public static byte HELPER_INIT;

        public static byte ECALL_CALL;
        public static byte ECALL_CALLVIRT;
        public static byte ECALL_NEWOBJ;
        public static byte ECALL_CALLVIRT_CONSTRAINED;

        public static byte FLAG_INSTANCE;

        public static byte EH_CATCH;
        public static byte EH_FILTER;
        public static byte EH_FAULT;
        public static byte EH_FINALLY;
    }
}