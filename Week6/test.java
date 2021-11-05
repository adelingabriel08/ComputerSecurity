import java.math.BigInteger;

class Program
{
    public static BigInteger[] factorize_modulus(BigInteger n, BigInteger eA, BigInteger dA)
    {
        BigInteger zero_bi = new BigInteger("0");
        BigInteger one_bi = new BigInteger("1");
        BigInteger two_bi = new BigInteger("2");
        BigInteger four_bi = new BigInteger("4");

        BigInteger temp,k;

        BigInteger p_plus_q, delta, root_1, root_2;

        temp = (dA.multiply(eA)).subtract(one_bi);

        if((temp.mod(n)).equals(zero_bi))
        {
            k=temp.divide(n);
        }
        else
        {
            k=(temp.divide(n)).add(one_bi);
        }

        System.out.println("Solved exercise k: " + k);

        p_plus_q = (n.add(one_bi)).subtract(temp.divide(k));

        System.out.println("Solved exercise p+q " + p_plus_q);

        delta = (p_plus_q.multiply(p_plus_q)).subtract(n.multiply(four_bi));

        root_1 = (p_plus_q.add(delta.sqrt())).divide(two_bi);
        root_2 = (p_plus_q.subtract(delta.sqrt())).divide(two_bi);

        System.out.println("Solved exercise root 1: " + root_1);
        System.out.println("Solved exercise root 2: " + root_2);

        if(root_1.multiply(root_2).equals(n))
        {
            System.out.println("Solved exercose Factorization ok");
        }
        else
        {
            System.out.println("Solved exercise: Factorization NOK");
        }

        BigInteger[] factors = new BigInteger[2];

        factors[0]= root_1;
        factors[1] = root_2;

        return factors;
    }

    public static void main (String[] args)
    {
        BigInteger n = new BigInteger("837210799");
        BigInteger eA = new BigInteger("7");
        BigInteger dA = new BigInteger("478341751");
        BigInteger eB = new BigInteger("17");
        BigInteger dB;

        BigInteger[] factors;

        BigInteger n_ex2 = new BigInteger("5076313634899413540120536350051034312987619378778911504647420938544746517711031490115528420427319479274407389058253897498557110913160302801741874277608327");
        BigInteger e_ex2 = new BigInteger("3");
        BigInteger d_ex2 = new BigInteger("3384209089932942360080357566700689541991746252519274336431613959029831011807259226655786125050887727921274719751986104162037800807641522348207376583379547");

        factors = factorize_modulus(n_ex2, e_ex2, d_ex2);
    }
}