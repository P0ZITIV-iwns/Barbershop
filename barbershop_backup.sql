PGDMP         3                {            BarbershopDatabase    15.2    15.2 2    3           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            4           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            5           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            6           1262    16641    BarbershopDatabase    DATABASE     �   CREATE DATABASE "BarbershopDatabase" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
 $   DROP DATABASE "BarbershopDatabase";
                postgres    false            �            1259    16714    Client    TABLE     �   CREATE TABLE public."Client" (
    "Id" smallint NOT NULL,
    "FirstName" character varying(20) NOT NULL,
    "LastName" character varying(20),
    "Phone" character varying(10) NOT NULL
);
    DROP TABLE public."Client";
       public         heap    postgres    false            �            1259    16703    Employee    TABLE     �  CREATE TABLE public."Employee" (
    "Id" smallint NOT NULL,
    "FirstName" character varying(20) NOT NULL,
    "LastName" character varying(20) NOT NULL,
    "Patronymic" character varying(20) NOT NULL,
    "Post" character varying(20) NOT NULL,
    "Login" character varying(20) NOT NULL,
    "Password" character varying(40) NOT NULL,
    "Phone" character varying(10) NOT NULL,
    "Photo" text DEFAULT 'Images/Employees/noEmployeeImage.png'::text
);
    DROP TABLE public."Employee";
       public         heap    postgres    false            �            1259    16881    Entry    TABLE     >  CREATE TABLE public."Entry" (
    "Id" smallint NOT NULL,
    "DateTime" timestamp without time zone NOT NULL,
    "Amount" numeric(10,2) NOT NULL,
    "Status" character varying(20) DEFAULT 'Согласование'::character varying,
    "ID_client" integer,
    "ID_employee" integer,
    "ID_service" integer
);
    DROP TABLE public."Entry";
       public         heap    postgres    false            �            1259    16880    Entry_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."Entry_Id_seq"
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public."Entry_Id_seq";
       public          postgres    false    223            7           0    0    Entry_Id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public."Entry_Id_seq" OWNED BY public."Entry"."Id";
          public          postgres    false    222            �            1259    16904    Finance    TABLE     �   CREATE TABLE public."Finance" (
    "Id" smallint NOT NULL,
    "DateTime" timestamp without time zone NOT NULL,
    "ID_entry" integer
);
    DROP TABLE public."Finance";
       public         heap    postgres    false            �            1259    16903    Finance_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."Finance_Id_seq"
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public."Finance_Id_seq";
       public          postgres    false    225            8           0    0    Finance_Id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public."Finance_Id_seq" OWNED BY public."Finance"."Id";
          public          postgres    false    224            �            1259    16683    Product    TABLE     ,  CREATE TABLE public."Product" (
    "Id" smallint NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Description" character varying(250) NOT NULL,
    "Category" character varying(30) NOT NULL,
    "Price" numeric(10,2),
    "Image" text DEFAULT 'Images\Products\noProductImage.png'::text
);
    DROP TABLE public."Product";
       public         heap    postgres    false            �            1259    16643    Service    TABLE     �   CREATE TABLE public."Service" (
    "Id" smallint NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Duration" integer NOT NULL,
    "Price" numeric(10,2) NOT NULL,
    "Category" character varying(10)
);
    DROP TABLE public."Service";
       public         heap    postgres    false            �            1259    16713    client_id_seq    SEQUENCE     �   CREATE SEQUENCE public.client_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.client_id_seq;
       public          postgres    false    221            9           0    0    client_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.client_id_seq OWNED BY public."Client"."Id";
          public          postgres    false    220            �            1259    16702    employee_id_seq    SEQUENCE     �   CREATE SEQUENCE public.employee_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.employee_id_seq;
       public          postgres    false    219            :           0    0    employee_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.employee_id_seq OWNED BY public."Employee"."Id";
          public          postgres    false    218            �            1259    16682    product_id_seq    SEQUENCE     �   CREATE SEQUENCE public.product_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.product_id_seq;
       public          postgres    false    217            ;           0    0    product_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.product_id_seq OWNED BY public."Product"."Id";
          public          postgres    false    216            �            1259    16642    service_id_seq    SEQUENCE     �   CREATE SEQUENCE public.service_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.service_id_seq;
       public          postgres    false    215            <           0    0    service_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.service_id_seq OWNED BY public."Service"."Id";
          public          postgres    false    214            �           2604    16717 	   Client Id    DEFAULT     j   ALTER TABLE ONLY public."Client" ALTER COLUMN "Id" SET DEFAULT nextval('public.client_id_seq'::regclass);
 <   ALTER TABLE public."Client" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    221    220    221            �           2604    16706    Employee Id    DEFAULT     n   ALTER TABLE ONLY public."Employee" ALTER COLUMN "Id" SET DEFAULT nextval('public.employee_id_seq'::regclass);
 >   ALTER TABLE public."Employee" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    219    218    219            �           2604    16884    Entry Id    DEFAULT     j   ALTER TABLE ONLY public."Entry" ALTER COLUMN "Id" SET DEFAULT nextval('public."Entry_Id_seq"'::regclass);
 ;   ALTER TABLE public."Entry" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    223    222    223            �           2604    16907 
   Finance Id    DEFAULT     n   ALTER TABLE ONLY public."Finance" ALTER COLUMN "Id" SET DEFAULT nextval('public."Finance_Id_seq"'::regclass);
 =   ALTER TABLE public."Finance" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    224    225    225                       2604    16686 
   Product Id    DEFAULT     l   ALTER TABLE ONLY public."Product" ALTER COLUMN "Id" SET DEFAULT nextval('public.product_id_seq'::regclass);
 =   ALTER TABLE public."Product" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    216    217    217            ~           2604    16646 
   Service Id    DEFAULT     l   ALTER TABLE ONLY public."Service" ALTER COLUMN "Id" SET DEFAULT nextval('public.service_id_seq'::regclass);
 =   ALTER TABLE public."Service" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    215    214    215            ,          0    16714    Client 
   TABLE DATA           J   COPY public."Client" ("Id", "FirstName", "LastName", "Phone") FROM stdin;
    public          postgres    false    221   >9       *          0    16703    Employee 
   TABLE DATA           �   COPY public."Employee" ("Id", "FirstName", "LastName", "Patronymic", "Post", "Login", "Password", "Phone", "Photo") FROM stdin;
    public          postgres    false    219   �9       .          0    16881    Entry 
   TABLE DATA           q   COPY public."Entry" ("Id", "DateTime", "Amount", "Status", "ID_client", "ID_employee", "ID_service") FROM stdin;
    public          postgres    false    223   �;       0          0    16904    Finance 
   TABLE DATA           A   COPY public."Finance" ("Id", "DateTime", "ID_entry") FROM stdin;
    public          postgres    false    225   �<       (          0    16683    Product 
   TABLE DATA           ^   COPY public."Product" ("Id", "Name", "Description", "Category", "Price", "Image") FROM stdin;
    public          postgres    false    217   �<       &          0    16643    Service 
   TABLE DATA           R   COPY public."Service" ("Id", "Name", "Duration", "Price", "Category") FROM stdin;
    public          postgres    false    215   �G       =           0    0    Entry_Id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public."Entry_Id_seq"', 8, true);
          public          postgres    false    222            >           0    0    Finance_Id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public."Finance_Id_seq"', 6, true);
          public          postgres    false    224            ?           0    0    client_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.client_id_seq', 50, true);
          public          postgres    false    220            @           0    0    employee_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.employee_id_seq', 16, true);
          public          postgres    false    218            A           0    0    product_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.product_id_seq', 103, true);
          public          postgres    false    216            B           0    0    service_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.service_id_seq', 45, true);
          public          postgres    false    214            �           2606    16887    Entry Entry_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."Entry"
    ADD CONSTRAINT "Entry_pkey" PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public."Entry" DROP CONSTRAINT "Entry_pkey";
       public            postgres    false    223            �           2606    16909    Finance Finance_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Finance"
    ADD CONSTRAINT "Finance_pkey" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Finance" DROP CONSTRAINT "Finance_pkey";
       public            postgres    false    225            �           2606    16719    Client client_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."Client"
    ADD CONSTRAINT client_pkey PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public."Client" DROP CONSTRAINT client_pkey;
       public            postgres    false    221            �           2606    16711    Employee employee_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Employee"
    ADD CONSTRAINT employee_pkey PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Employee" DROP CONSTRAINT employee_pkey;
       public            postgres    false    219            �           2606    16691    Product product_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT product_pkey PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Product" DROP CONSTRAINT product_pkey;
       public            postgres    false    217            �           2606    16648    Service service_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Service"
    ADD CONSTRAINT service_pkey PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Service" DROP CONSTRAINT service_pkey;
       public            postgres    false    215            �           2606    16888    Entry Entry_ID_client_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Entry"
    ADD CONSTRAINT "Entry_ID_client_fkey" FOREIGN KEY ("ID_client") REFERENCES public."Client"("Id");
 H   ALTER TABLE ONLY public."Entry" DROP CONSTRAINT "Entry_ID_client_fkey";
       public          postgres    false    221    223    3214            �           2606    16893    Entry Entry_ID_employee_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Entry"
    ADD CONSTRAINT "Entry_ID_employee_fkey" FOREIGN KEY ("ID_employee") REFERENCES public."Employee"("Id");
 J   ALTER TABLE ONLY public."Entry" DROP CONSTRAINT "Entry_ID_employee_fkey";
       public          postgres    false    3212    223    219            �           2606    16898    Entry Entry_ID_service_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Entry"
    ADD CONSTRAINT "Entry_ID_service_fkey" FOREIGN KEY ("ID_service") REFERENCES public."Service"("Id");
 I   ALTER TABLE ONLY public."Entry" DROP CONSTRAINT "Entry_ID_service_fkey";
       public          postgres    false    3208    223    215            �           2606    16910    Finance Finance_ID_entry_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Finance"
    ADD CONSTRAINT "Finance_ID_entry_fkey" FOREIGN KEY ("ID_entry") REFERENCES public."Entry"("Id");
 K   ALTER TABLE ONLY public."Finance" DROP CONSTRAINT "Finance_ID_entry_fkey";
       public          postgres    false    3216    223    225            ,   �   x����0E��aPN���0�B�$f�
E����v#���}��%.X��N��%\��h�`�u�BU4�Z�B����	�FW�T�z��%�(�f�d���&'��ja��'~�y�����r	A9����W���>�;c	�x`�?��V�      *   �  x��S�n�0�����U)Ҕ�营S� 1�i�G�7%AѥC۩KP�P�(?�3ߨGJB��h8R�����
+x�6[[h���_�����ڜ�sSBeΡ1%I��LuQ��S.hH�A����7o��X/��S�߻+�F^�����;s���7[qW=���`�_G�'B�x{��*�	�H⃞���OSb���c&����ǆ�tukl���7����"����좑��L%*���S�-��P�@�c�\�2y��A��Jq������\Y�yJ�Y�ϴ��%����@� T��l���������������W�Y�*�	F)iE;\8�4��s���uPv����;9�8������j��C�,�lI�(cɄ���?��� �˂�奕���HTvLMi�<��F9u�z��)i���8@	�����֝׈X�v<��/�^7�Ym^��pl�@]Zu|�<
�r�w���<���/3      .   �   x�uл�@�ڞ�H����1� ؀
f��"��n#0�@�,��'����hC��F��2Qk���Tne��z*sy�	b��,74��,L�&�� ���D|ݵ��-\�  ��R���>r��[�]).��]�ݗ�zt�,0��:��������O,�ZD|�s�      0   ?   x�3�4202�50�52V04�20 "Nc.S����������1X܄�.nd�`h
Uo����� ^6      (   �
  x��Z�n�>��b`Is��H����� �!��
߸��H��E9�c���K9;��
=��'I�W�=ݳ3˥(!��r9�]�]��W?���������_<|���{���Il~0I�3Y�_��W&3�،��\�Ԍ�/fZtM��Ib|��ЁIcs��)���l&�qd�n']�Qq�#�B��hq����i\��v���.�&'	M�M���{j�X~\� �9	��HT[��RqL���F�1�kf�>�Mf&k���B�׉�!$������œ�S>#iײ�<��E{��o��ю��jqCRV�X�@ѧYM{I��DI��P+=�!�V���N�������Z����_��}�峧���W��>��۫|���N��i��ط*}�]X�V)4��;���Ȝ�i0bV�)K˴�'��K�
+Ȯl��8�i�)��e�a����?^�)�����bR����xK�A<��&٬똄Ә�5f�����̓�uO?X�uN�_����ðz�&];��8L/���b�t �����>�����*�sU�$��Ą>��#o�}�>����	;'�©�ՄaRP|W�p��� d���&��`{����T0�A��;�������"=�COу�x�$��ӽov�?�j/6��zl��i3Nm!{yKF�@��cѤr�}�A�<�h� IO3x��m�i	|>�������h�W4�8�X�w �0���]��y+d�S<�9�L�$��/��!%�'����\4,{���H����4tHlʠ���0��� 1��Ơ��A� '��!+c$<��v�v`��¿�u�� ���Xz�� �I��v�����G��m�~��}l�¢6��j��5�J$X��9/��a�����q�`�x���_�K"J�V��gϾ������\��t�d	ؼ�t��?m���4G�>�3�\Z#ξ`��5
���&�GX��
�9Tg���i�ɻa�(�`�(NK7s��E�����- \Bm"H����Z8�g�ՠ��~ �rnwr:�ހ���1��>�Jc.X*D��y\�J�9��
� ��XY'}^�՜�6��}/�X����r<���1!YU�u[�}��K��Փz@�9�F�3�pz<�9%��.�iL�'��Y"���N]Ty#<z���
��_��{��  R.�&s��=�R���:Z؉�2]�u�\����K=Z���|�z�v��k��Bid�9�m��
v�K��E(fr�Nc���j��Gi�pP�F��������r��$�o)i�t[^jb�rbm��Z��B����+�$ !�蓁Ϥ�U�����T�;�)>��i�_���"�#�LZ1:`y\:/o�p�	p��%%�4���<2���no��:@��v;*��3�2�$A7gO���h�.C�����0��s�2����h)�.-��m^K�p}��BzpA��Y���~��Ak����\x����ٙI����� �"�ͨ!��\tr8���_�t,[,��!?U[��Aƛ�bca�U���i��|�0iaü�����0�T��*V�v�`��t��6��3��W�H`��8�,O����:�ֲ;6)ﵦ�W��f| TL��Y��A0,���H��u�0\����u+� ���d��XXJ��t5��j�->�|�u��R���F=�����o9>/�`|7(���H5����U�H�zREm܎l�5E`@���`.bS�,���Z�f�Y����ck�2��� uW
>,$���!�O��O�������H��"�j�����M/�b{�����Qm8h2�'�k�B�wg� ~��pR��_�`�9�z�V��H�^Tj�"e��!I��6���P��D��ߗT��� K��D��i>+0T@�T;��Z9ɕ����[��Rp�Dw #���cs~���K���ɷz;�Ki[I���քU3?��
2�D�n�lP�
}Ba�w�Lj�K�%�9�a�y��|"�F����]��s2�9��$ɹ}�UD�T���:�){���v���a�~�����7B�tL�ˇ�þ�]E���>������߮�B�﫬����	�f����uY�M4*��DMH �
y1��Unn��+�lC@�8��W����3<'�d`���#)0��G���+�L�����J�xŨ(�[ =}�DZ�~Xں��ФM����d�Ǥv�J�;�t�s ��{^�Ñ����{V
+|#F�Ysb�ku`�ր��X�q�2ܡ��!,e����V�Urԓ�|�QBR��V�v�i�4��\�A�ԒF�T�.��6M��+O��ۺ���+3���1�ķ���w Տ��$��fb=�c�S�{��]��.��F��������pŭ��)�·�
N�%q_��������?{��=^RQ'xjw��+���9��H��Y󊇷b|:�K�8��@�69#r[��֘�V��+�W>a�'���W;�)�iߠv�۔�~�t����N��*E炲��J	��[����A�#������JK����q�jTo�h�ɮu���e�-��!h��w��mx֚����,w�1w�#��ل,3�O��p͙Iu�1XB;A�h�e毒5�ck�𐄠ӹ��Ʒ�Ȃ�q��4��{��pd����Wo \7F�"%��g���U�Q�@斯8�v5x�@�^B��7��i�߯^��p�Z	{��k*�N�ݦz�l߆^$����{��y^H      &   �  x��V�r�@<����\\��a��|���Nʉ��C��q�r'��
���Y�@)��Bڝ�����x�d�6��V���O��
��G����}Z���1��
3k;�/�� ����h��5��&$fvT��N1�t��%Zzܮ�t���3�(�S�{DӋI<zDtm?�y�ؑ��Uke������x�5�}�*ڨMՒǭD�D9�>8ͨi!F���(�F���v���2X$V��"�+���su��p�ى��g�U]�|G6R����z:1��L]������C��N%O�~���tx��9$��ɘ��p	\�B~�z ͭ��4����}5<%������~ޚʞ���m"�.Bu����z=����t"�:p�N5L܇��'��H�A�e�t0�S�ڱ��|���a�H��҆,v��^T�
�<��-����sX$���7�� �8:�ElS�[6���+��7<e���7x,�Ҿﰤ����.z�00���
�N@֬C��{�(�D�;Kr���/�(��*uE��=_WG��0��ѩ�`��-�ӏ��M�S�;r��ZMF#ݍ��#�,X��2���Sɻ���]m9�>�o" ��� ���v�2���f����H�xE�5&����Da�z��V��̥k̭3�u���9R�q���|�}��Nf�����rlႛ	���C�->F��ڇ0�`�'C��o�����'����r0��W��     